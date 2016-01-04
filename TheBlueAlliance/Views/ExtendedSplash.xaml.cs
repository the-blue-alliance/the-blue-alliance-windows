using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Threading;
using System.Reactive.Linq;
using System.Diagnostics;
using TBA.DataServices;
using TBA.Models;
using System.Collections;
using Windows.Storage;
using TBA.Caches;
using System.Reactive.Concurrency;
using TBA.Common;
using SQLiteNetExtensions.Extensions;
using TBA.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TBA.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal AppShell shell;
        public ExtendedSplashViewModel ViewModel { get; set; }

        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            DataContext = this;
            ViewModel = new ExtendedSplashViewModel();

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;
            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                PositionRing();
            }
            
            // Create a Frame to act as the navigation context 
            shell = new AppShell();
            DismissExtendedSplash();
        }

        void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;
        }

        void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (splash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                PositionRing();
            }
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        async void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // Perform setup if this is the first launch
            if ((bool)localSettings.Values["FirstLaunch"] == true)
            {
                ViewModel.LoadingText = "Performing initial setup...";
                StatusHelper statusHelper = new StatusHelper();
                statusHelper.UpdateStatus();

                DataStoreHelper.CreateTable<EventModel>("EventModel");
                EventHttpClient eventHttpClient = new EventHttpClient();
                EventListResponse events = await eventHttpClient.GetAll();
                DataStoreHelper.InsertBulk(events.Data);
                
                DataStoreHelper.CreateTable<TeamModel>("TeamModel");
                TeamHttpClient teamHttpClient = new TeamHttpClient();
                List<TeamModel> teams = teamHttpClient.GetAll();
                DataStoreHelper.InsertBulk(teams);

                DataStoreHelper.CreateTable<DistrictListModel>("DistrictListModel");
                DistrictHttpClient districtHttpClient = new DistrictHttpClient();
                DistrictListResponse districts = await districtHttpClient.GetAll();
                foreach(DistrictListModel district in districts.Data)
                {
                    district.PK = district.Key + districts.Year;
                    DataStoreHelper.db.Insert(district);
                    district.DistrictEvents = DataStoreHelper.db.Query<EventModel>("SELECT * FROM `EventModel` WHERE `EventDistrictString` LIKE ?", district.Name);

                    DataStoreHelper.db.UpdateWithChildren(district);
                }

                localSettings.Values["FirstLaunch"] = false; // Flip the first launch flag for subsequent runs
            }
            ViewModel.LoadingText = "Done!";
            localSettings.Values["LoadingApp"] = false;
        }

        async void DismissExtendedSplash()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            // A bit dirty, but it works for now. Checks the loading setting every second, then continues into the app.
            while ((bool)localSettings.Values["LoadingApp"] == true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            // Navigate to mainpage
            shell.AppFrame.Navigate(typeof(Landing.OffseasonLanding));
            // Place the frame in the current Window
            Window.Current.Content = shell;
        }
    }
}

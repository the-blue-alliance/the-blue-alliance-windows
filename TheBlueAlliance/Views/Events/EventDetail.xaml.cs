using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TBA.Caches;
using TBA.ViewModels;

namespace TBA.Views.Events
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventDetail : Page
    {
        public EventDetail()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string key = e.Parameter.ToString();

            EventCache eventCache = new EventCache();
            EventHttpClient eventHttpClient = new EventHttpClient();
            IScheduler scheduler = Scheduler.Default;

            IEventClient eventClient = new RxEventClient(eventCache, eventHttpClient, scheduler);
            EventViewModel singleEvent = new EventViewModel(eventClient, key);

            eventName.Text = singleEvent.Name.ToString();
            eventLocation.Text = singleEvent.Location.ToString();
            eventStartdate.Text = singleEvent.StartDate.ToString("D");
        }
    }
}

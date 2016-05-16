using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using TBA.Models;
using TBA.ViewModels;

namespace TBA.Views.Events
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventListing : Page
    {
        public EventListViewModel Events { get; set; }

        public EventListing()
        {
            InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Events = new EventListViewModel();
            EventList.ItemsSource = Events.Data;
            SeasonSelector.ItemsSource = Constants.SeasonList();
            SeasonSelector.SelectedItem = Events.SelectedSeason;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                Events.SelectedSeason = Convert.ToInt32(e.Parameter);
            }
        }

        private void EventList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as EventModel;
            this.Frame.Navigate(typeof(EventDetail), item.Key.ToString());
        }

        private void SeasonSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Events.SelectedSeason = Convert.ToInt32(SeasonSelector.SelectedItem);
        }
    }
}

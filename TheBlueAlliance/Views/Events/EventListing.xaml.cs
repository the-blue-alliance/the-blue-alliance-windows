using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TBA.Models;
using TBA.ViewModels;

namespace TBA.Views.Events
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventListing : Page
    {
        public EventListing()
        {
            InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            EventListViewModel events = new EventListViewModel();
            EventList.ItemsSource = events.Data;
        }

        private void EventList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as EventModel;
            //string k = enter.Text;
            this.Frame.Navigate(typeof(EventDetail), item.Key.ToString());
        }
    }
}

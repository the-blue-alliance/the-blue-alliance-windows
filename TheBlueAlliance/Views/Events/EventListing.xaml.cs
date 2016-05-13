using Windows.UI.Xaml.Controls;
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
            EventListViewModel events = new EventListViewModel();
            EventList.ItemsSource = events.Data;
        }
    }
}

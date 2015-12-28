using Windows.UI.Xaml.Controls;
using System.Reactive.Concurrency;
using TBA.Caches;
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
            this.InitializeComponent();
            EventCache eventCache = new EventCache();
            EventHttpClient eventHttpClient = new EventHttpClient();
            IScheduler scheduler = Scheduler.Default;

            IEventClient eventClient = new RxEventClient(eventCache, eventHttpClient, scheduler);
            EventViewModel Event = new EventViewModel(eventClient, "2015ohcl");
        }

        //public interface IRatingClient
        //{
        //    IObservable<EventModel> GetEvent(string eventKey);
        //}

        //EventListViewModel EventList { get; set; }
    }
}

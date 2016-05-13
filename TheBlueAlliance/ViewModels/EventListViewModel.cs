using System;
using DynamicData;
using DynamicData.Binding;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using TBA.DataServices;
using TBA.Models;

namespace TBA.ViewModels
{
    public class EventListViewModel : AbstractNotifyPropertyChanged
    {
        private readonly ReadOnlyObservableCollection<EventModel> _data;

        public EventListViewModel()
        {
            var eventList = new SourceList<EventModel>();
            var eventQuery = DataStoreHelper.db.Table<EventModel>();
            foreach (var eventResult in eventQuery)
                eventList.Add<EventModel>(eventResult);
            var eventObservables = eventList.Connect()
                .Bind(out _data)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<EventModel> Data
        {
            get { return _data; }
        }
    }
}

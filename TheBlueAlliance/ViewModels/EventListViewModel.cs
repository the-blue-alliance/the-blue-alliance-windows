using System;
using System.Linq;
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
        private SourceList<EventModel> eventList = new SourceList<EventModel>();
        private int _selectedSeason;

        public EventListViewModel()
        {
            SelectedSeason = Constants.MaxSeason();

            var filter = this.WhenValueChanged(s => s.SelectedSeason)
                .Select(BuildFilter);

            var eventQuery = DataStoreHelper.db.Table<EventModel>();

            foreach (var eventResult in eventQuery)
                eventList.Add<EventModel>(eventResult);

            var eventObservables = eventList.Connect()
                .Filter(filter)
                .Bind(out _data)
                .Subscribe();
        }

        private static Func<EventModel, bool> BuildFilter(int SelectedSeason)
        {
            return e => e.Year.Equals(SelectedSeason);
        }

        public ReadOnlyObservableCollection<EventModel> Data
        {
            get { return _data; }
        }

        public int SelectedSeason {
            get { return _selectedSeason; }
            set
            {
                _selectedSeason = value;
                OnPropertyChanged("SelectedSeason");
            }
        }
    }
}

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

        //EventList eventlist;

        //public EventListViewModel()
        //{
        //    eventlist = new EventList();
        //    _SelectedIndex = -1;
        //    // Load the database
        //    foreach (var eventmodel in eventlist.Events)
        //    {
        //        var np = new EventViewModel(eventmodel);
        //        np.PropertyChanged += Event_OnNotifyPropertyChanged;
        //        _Events.Add(np);
        //    }
        //}

        //ObservableCollection<EventViewModel> _Events = new ObservableCollection<EventViewModel>();
        //public ObservableCollection<EventViewModel> Events
        //{
        //    get { return _Events; }
        //    set { SetProperty(ref _Events, value); }
        //}

        //int _SelectedIndex;
        //public int SelectedIndex
        //{
        //    get { return _SelectedIndex; }
        //    set { if (SetProperty(ref _SelectedIndex, value)) { RaisePropertyChanged(nameof(SelectedEvent)); } }
        //}

        //public EventViewModel SelectedEvent
        //{
        //    get { return (_SelectedIndex >= 0) ? _Events[_SelectedIndex] : null; }
        //}

        //public void Add()
        //{
        //    var eventmodel = new EventViewModel();
        //    eventmodel.PropertyChanged += Event_OnNotifyPropertyChanged;
        //    Events.Add(eventmodel);
        //    eventlist.Add(eventmodel);
        //    SelectedIndex = Events.IndexOf(eventmodel);
        //}

        //public void Delete()
        //{
        //    if (SelectedIndex != -1)
        //    {
        //        var eventmodel = Events[SelectedIndex];
        //        Events.RemoveAt(SelectedIndex);
        //        eventlist.Delete(eventmodel);
        //    }
        //}

        //void Event_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        //{
        //    eventlist.Update((EventViewModel)sender);
        //}
    }
}

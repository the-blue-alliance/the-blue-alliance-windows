using System.Collections.Generic;
using System.Linq;
using TBA.DataServices;
using TBA.Models;

namespace TBA.Caches
{
    public interface IEventCache
    {
        bool HasCached(string key);
        EventModel GetCachedItem(string eventKey);
        void Put(EventModel updatedModel);
    }

    public class EventCache : IEventCache
    {
        public bool HasCached(string eventKey)
        {
            List<EventModel> _Event = DataStoreHelper.GetDataStore().Query<EventModel>("SELECT * FROM EventModel WHERE Key = ?", eventKey);
            
            if (_Event.First() != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public EventModel GetCachedItem(string eventKey)
        {
            EventModel _Event = DataStoreHelper.GetDataStore().Query<EventModel>("SELECT * FROM EventModel WHERE Key = ?", eventKey).FirstOrDefault();

            return _Event;
        }

        public void Put(EventModel updatedModel)
        {
            EventModel _event = DataStoreHelper.GetDataStore().Query<EventModel>("SELECT * FROM EventModel WHERE Key = ?", updatedModel.Key).FirstOrDefault();
            if (_event == null)
            {
                DataStoreHelper.GetDataStore().Insert(updatedModel);
                return;
            }
            DataStoreHelper.GetDataStore().Update(updatedModel);
        }
    }
}

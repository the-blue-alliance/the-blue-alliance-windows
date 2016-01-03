using System.Collections.Generic;
using System.Diagnostics;

namespace TBA.Models
{
    internal class EventModelComparer : IEqualityComparer<EventModel>
    {
        public bool Equals(EventModel x, EventModel y)
        {
            return x.Key == y.Key
                   && x.Name == y.Name;
        }
        public int GetHashCode(EventModel obj)
        {
            Debug.WriteLine(obj.GetHashCode());
            return obj.GetHashCode();
        }
    }

    internal class TeamModelComparer : IEqualityComparer<TeamModel>
    {
        public bool Equals(TeamModel x, TeamModel y)
        {
            return x.Key == y.Key
                   && x.Name == y.Name;
        }
        public int GetHashCode(TeamModel obj)
        {
            Debug.WriteLine(obj.GetHashCode());
            return obj.GetHashCode();
        }
    }
}

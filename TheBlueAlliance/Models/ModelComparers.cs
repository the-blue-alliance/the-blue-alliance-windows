﻿using System.Collections.Generic;
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
}

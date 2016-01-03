using System;
using System.Collections.Generic;

namespace TBA.Models
{

    public class EventResponse
    {
        public bool IsSuccessful { get; set; }
        public EventModel Data { get; set;}
        public Exception Exception { get; set; }
    }

    public class EventListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<EventModel> Data { get; set; }
        public Exception Exception { get; set; }
    }
}

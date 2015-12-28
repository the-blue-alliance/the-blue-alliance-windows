using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using TBA.Caches;
using TBA.DataServices;

namespace TBA.Models
{
    // Represents DB tables
    public class EventDB
    {
        [PrimaryKey, AutoIncrement]
        public int EventPK { get; set; }
        [Indexed]
        public string Id { get; set; }
        public string Website { get; set; }
        public bool Official { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string FacebookEid { get; set; }
        public string EventDistrictString { get; set; }
        public string VenueAddress { get; set; }
        public string EventDistrict { get; set; }
        public string Location { get; set; }
        public string EventCode { get; set; }
        public int Year { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<EventWebcast> WebcastKey { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<EventFinalsAlliance> Alliances { get; set; }
        public string EventTypeString { get; set; }
        public string StartDate { get; set; }
        public int EventType { get; set; }

        public class EventWebcast
        {
            [PrimaryKey, AutoIncrement]
            public int EventWebcastPK { get; set; }

            [ForeignKey(typeof(EventModel))]
            public string EventKey { get; set; }
            public string Type { get; set; }
            public string Channel { get; set; }

            [ManyToOne]
            public EventModel Event { get; set; }
        }

        public class EventFinalsAlliance
        {
            [PrimaryKey, AutoIncrement]
            public int EventFinalsAlliancePK { get; set; }

            [ForeignKey(typeof(EventModel))]
            public string EventKey { get; set; }
            [OneToMany(CascadeOperations = CascadeOperation.All)]
            public List<EventFinalsAllianceDecline> Declines { get; set; }
            [OneToMany(CascadeOperations = CascadeOperation.All)]
            public List<EventFinalsAlliancePick> Picks { get; set; }

            [ManyToOne]
            public EventModel Event { get; set; }
        }

        public class EventFinalsAlliancePick
        {
            [PrimaryKey, AutoIncrement]
            public int EventFinalsAlliancePickPK { get; set; }
            public string Team { get; set; }

            [ForeignKey(typeof(EventFinalsAlliance))]
            public string EventFinalsAllianceKey { get; set; }
            [ForeignKey(typeof(EventModel))]
            public string EventKey { get; set; }

            [ManyToOne]
            public EventFinalsAlliance EventFinalsAlliance { get; set; }
        }

        public class EventFinalsAllianceDecline
        {
            [PrimaryKey, AutoIncrement]
            public int EventFinalsAllianceDeclinePK { get; set; }
            public string Team { get; set; }

            [ForeignKey(typeof(EventFinalsAlliance))]
            public string EventFinalsAllianceKey { get; set; }
            [ForeignKey(typeof(EventModel))]
            public string EventKey { get; set; }

            [ManyToOne]
            public EventFinalsAlliance EventFinalsAlliance { get; set; }
        }
    }
}

using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Newtonsoft.Json;
using TBA.Caches;

namespace TBA.Models
{
    // Represents DB tables
    public class EventModel : IEntity<string>
    {
        [PrimaryKey, AutoIncrement]
        public int EventPK { get; set; }

        [JsonProperty("key")]
        [Indexed]
        public string Key { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("official")]
        public bool Official { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("facebook_eid")]
        public string FacebookEid { get; set; }

        [JsonProperty("event_district_string")]
        public string EventDistrictString { get; set; }

        [JsonProperty("venue_address")]
        public string VenueAddress { get; set; }

        [JsonProperty("event_district")]
        public int? EventDistrict { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("event_code")]
        public string EventCode { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("webcast")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public Webcast[] Webcasts { get; set; }

        [JsonProperty("alliances")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public Alliance[] Alliances { get; set; }

        [JsonProperty("event_type_string")]
        public string EventTypeString { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("event_type")]
        public int EventType { get; set; }

        public class Webcast
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("channel")]
            public string Channel { get; set; }
        }

        public class Alliance
        {
            [JsonProperty("declines")]
            public string[] Declines { get; set; }

            [JsonProperty("picks")]
            public string[] Picks { get; set; }
        }
    }

    public class EventWebcast
    {
        [PrimaryKey, AutoIncrement]
        public int EventWebcastPK { get; set; }
        [ForeignKey(typeof(EventModel))]
        public string EventKey { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [ManyToOne]
        public EventModel Event { get; set; }
    }

    public class EventAlliances
    {
        [PrimaryKey, AutoIncrement]
        public int EventAlliancePK { get; set; }

        [ForeignKey(typeof(EventModel))]
        public string EventKey { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [JsonProperty("declines")]
        public List<EventAllianceDecline> Declines { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [JsonProperty("picks")]
        public List<EventAlliancePick> Picks { get; set; }

        [ManyToOne]
        public EventModel Event { get; set; }
    }

    public class EventAlliancePick
    {
        [PrimaryKey, AutoIncrement]
        public int EventAlliancePickPK { get; set; }
        public string Team { get; set; }

        [ForeignKey(typeof(EventAlliances))]
        public string EventFinalsAllianceKey { get; set; }
        [ForeignKey(typeof(EventModel))]
        public string EventKey { get; set; }

        [ManyToOne]
        public EventAlliances EventAlliances { get; set; }
    }

    public class EventAllianceDecline
    {
        [PrimaryKey, AutoIncrement]
        public int EventAllianceDeclinePK { get; set; }
        public string Team { get; set; }

        [ForeignKey(typeof(EventAlliances))]
        public string EventFinalsAllianceKey { get; set; }
        [ForeignKey(typeof(EventModel))]
        public string EventKey { get; set; }

        [ManyToOne]
        public EventAlliances EventAlliances { get; set; }
    }
}

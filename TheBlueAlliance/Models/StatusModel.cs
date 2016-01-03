using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLiteNetExtensions.Attributes;

namespace TBA.Models
{
    public class Android
    {
        [JsonProperty("min_app_version")]
        public int MinAppVersion { get; set; }

        [JsonProperty("latest_app_version")]
        public int LatestAppVersion { get; set; }
    }

    public class Ios
    {
        [JsonProperty("min_app_version")]
        public int MinAppVersion { get; set; }

        [JsonProperty("latest_app_version")]
        public int LatestAppVersion { get; set; }
    }

    public class StatusModel
    {
        [JsonProperty("max_season")]
        public int MaxSeason { get; set; }

        [JsonProperty("android")]
        public Android Android { get; set; }

        [JsonProperty("down_events")]
        public object[] DownEvents { get; set; }

        [JsonProperty("ios")]
        public Ios Ios { get; set; }

        [JsonProperty("is_datafeed_down")]
        public bool IsDatafeedDown { get; set; }
    }
}

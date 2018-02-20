using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceXLaunchSearchApi.Data.Models.SpaceX
{
    public class LaunchModel : BaseModel
    {
        [JsonProperty(PropertyName = "flight_number")]
        public int FlightNumber { get; set; }
        [JsonProperty(PropertyName = "launch_date_utc")]
        public DateTime LaunchUtcDateTime { get; set; }
        [JsonProperty(PropertyName = "launch_date_local")]
        public DateTime LaunchLocalDateTime { get; set; }
        [JsonProperty(PropertyName = "rocket")]
        public RocketModel Rocket { get; set; }
        [JsonProperty(PropertyName = "launch_site")]
        public LaunchSiteModel LaunchSite { get; set; }
        [JsonProperty(PropertyName = "details")]
        public string Details { get; set; }
        [JsonProperty(PropertyName = "launch_success")]
        public bool LaunchSuccess { get; set; }
        [JsonProperty(PropertyName = "links")]
        public IDictionary<string, string> Links { get; set; }
    }
}

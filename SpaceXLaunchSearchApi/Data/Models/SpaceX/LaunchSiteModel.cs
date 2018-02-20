using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceXLaunchSearchApi.Data.Models.SpaceX
{
    public class LaunchSiteModel : BaseModel
    {
        [JsonProperty(PropertyName = "site_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "site_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "site_name_long")]
        public string LongName { get; set; }
    }
}

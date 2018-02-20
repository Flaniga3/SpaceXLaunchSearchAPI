using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceXLaunchSearchApi.Data.Models.SpaceX
{
    public class RocketModel : BaseModel
    {
        [JsonProperty(PropertyName = "rocket_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "rocket_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "rocket_type")]
        public string Type { get; set; }
    }
}

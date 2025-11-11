using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoadStatus.Models
{
    public class RoadCorridor
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty ;

        [JsonProperty("statusSeverity")]
        public string StatusSeverity { get; set; } = string.Empty;

        [JsonProperty("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; } = string.Empty;

        [JsonProperty("bounds")]
        public string Bounds { get; set; } = string.Empty;

        [JsonProperty("envelope")]
        public string Envelope { get; set; } = string.Empty;

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;
    }
}

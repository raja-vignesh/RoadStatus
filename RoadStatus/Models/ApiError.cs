using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoadStatus.Models
{
    public sealed class ApiError
    {
        [JsonProperty("timestampUtc")]
        public string TimestampUtc { get; set; } = string.Empty;

        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; } = string.Empty ;

        [JsonProperty("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        [JsonProperty("httpStatus")]
        public string HttpStatus { get; set; } = string.Empty;

        [JsonProperty("relativeUri")]
        public string RelativeUri { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}

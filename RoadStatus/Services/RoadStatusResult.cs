using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus.Services
{
    public class RoadStatusResult
    {
        public string RoadDisplayName { get; set; } = string.Empty;
        public string RoadStatus { get; set; } = string.Empty;
        public string RoadStatusDescription { get; set; } = string.Empty;
        public bool IsValid { get; set; }

        public string? ErrorMessage { get; set; }
    }
}

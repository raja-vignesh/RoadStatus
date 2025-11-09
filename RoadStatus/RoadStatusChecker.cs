using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadStatus.ApiServices;
using RoadStatus.Services;
using RoadStatus.Services.ServiceInterfaces;

namespace RoadStatus
{
    public class RoadStatusChecker
    {
        private readonly ITflApiService _apiService;

        public RoadStatusChecker(ITflApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<RoadStatusResult> CheckStatus(string roadId)
        {
            var (road, error) = await _apiService.GetRoadStatusAsync(roadId);
            return new RoadStatusResult { RoadDisplayName = road.DisplayName, RoadStatus = road.StatusSeverity, RoadStatusDescription = road.StatusSeverityDescription };
        }
    }
}

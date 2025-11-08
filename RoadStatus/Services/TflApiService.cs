using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadStatus.Models;

namespace RoadStatus.ApiServices
{
    public class TflApiService
    {
        public Task<(RoadCorridor, string)> GetRoadStatusAsync(string roadId)
        {
            throw new NotImplementedException();
        }
    }
}

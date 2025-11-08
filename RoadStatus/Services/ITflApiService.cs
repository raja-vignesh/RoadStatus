using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadStatus.Models;

namespace RoadStatus.Services
{
    public interface ITflApiService
    {
        public Task<(RoadCorridor, string)> GetRoadStatusAsync(string roadId);
    }
}

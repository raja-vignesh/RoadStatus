using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RoadStatus.Models;
using RoadStatus.Services.ServiceInterfaces;

namespace RoadStatus.ApiServices
{
    //TflApiService, connects to the TfL API to get road status information.
    //It uses an HttpClient to send requests and an API key from the configuration.
    public class TflApiService : ITflApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _appKey;

        public TflApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _appKey = configuration["TflApiSettings:AppKey"]!;
            _httpClient.BaseAddress = new Uri(configuration["TflApiSettings:BaseUrl"]!);
        }

        //GetRoadStatusAsync takes a road ID, calls the TfL API, and reads the response.
        //If the response is successful, it returns the road details.If there’s an error, it returns an error object instead.
        public async Task<(RoadCorridor? road, ApiError? error)> GetRoadStatusAsync(string roadId)
        {
            var url = $"Road/{roadId}?&app_key={_appKey}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var roads = JsonConvert.DeserializeObject<List<RoadCorridor>>(content);
                return (roads?.FirstOrDefault(), null);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ApiError>(content);
                return (null, error);
            }
        }

        
    }
}

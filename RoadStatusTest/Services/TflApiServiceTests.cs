using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq.Protected;
using Moq;
using RoadStatus.ApiServices;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoadStatusTest.Services
{
    public class TflApiServiceTests
    {
        //This test checks that when a valid road ID(“A2”) is requested, the TflApiService returns the
        //correct road data.It mocks an HTTP response from the TfL API with sample JSON showing the road status as “Good.”
        [Fact]
        public async Task GetRoadStatusAsync_ValidRoad_ReturnsRoadData()
        {
            // Arrange


            /*var handlerMock = new Mock<HttpClient>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{
                ""id"": ""a2"",
                ""displayName"": ""A2"",
                ""statusSeverity"": ""Good"",
                ""statusSeverityDescription"": ""No Exceptional Delays""
            }]")
            };*/

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{
                ""id"": ""a2"",
                ""displayName"": ""A2"",
                ""statusSeverity"": ""Good"",
                ""statusSeverityDescription"": ""No Exceptional Delays""
            }]")
            };

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.tfl.gov.uk/")

            };

            // Use the simpler in-memory configuration approach
            var inMemorySettings = new Dictionary<string, string?> {
                {"TflApiSettings:AppKey", "test_key"},
                {"TflApiSettings:BaseUrl", "https://api.tfl.gov.uk/"}
            };

            Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new TflApiService(httpClient, configuration);

            // Act
            var (road, error) = await service.GetRoadStatusAsync("A2");

            // Assert
            Assert.NotNull(road);
            Assert.Null(error);
            Assert.Equal("A2", road.DisplayName);
            Assert.Equal("Good", road.StatusSeverity);
        }

        //This test checks that when an in valid road ID(“A23”) is requested, the TflApiService returns the 404 response
        [Fact]
        public async Task GetRoadStatusAsync_InvalidRoad_ReturnsError()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(@"{
                ""httpStatusCode"": 404,
                ""message"": ""The following road id is not recognised: A233""
            }")
            };

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.tfl.gov.uk/")
            };

            var inMemorySettings = new Dictionary<string, string?> {
                {"TflApiSettings:AppKey", "test_key"},
                {"TflApiSettings:BaseUrl", "https://api.tfl.gov.uk/"}
            };

            Microsoft.Extensions.Configuration.IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var service = new TflApiService(httpClient, configuration);

            // Act
            var (road, error) = await service.GetRoadStatusAsync("A233");

            // Assert
            Assert.Null(road);
            Assert.NotNull(error);
            Assert.Equal(404, error.HttpStatusCode);
            Assert.Contains("not recognised", error.Message);
        }
    }
}

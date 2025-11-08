using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RoadStatus.ApiServices;
using RoadStatus.Models;
using RoadStatus.Services;

namespace RoadStatusTest.Features
{
    
    public class RoadStatusFeatureTests
    {
        [Fact]
        // This unit test verifes that when a valid road ID ("A2") is requested,
        // the RoadStatusChecker correctly retrieve the road status from the mocked ITflApiService
        // and return the expected DisplayName in the result.
        public async Task GivenValidRoadId_WhenGettingRoadStatus_ThenShowCorrectDisplayName()
        {
            //Arrange
            var mockApiService = new Mock<ITflApiService>();
            var expectedRoad = new RoadCorridor { DisplayName = "A2", StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays" };
            mockApiService.Setup(x => x.GetRoadStatusAsync("A2")).ReturnsAsync((expectedRoad, null));
            var roadStatusChecker = new RoadStatusChecker(mockApiService.Object);

            // Act
            var result = await roadStatusChecker.CheckStatus("A2");

            // Assert
            Assert.Equal("A2", result.RoadDisplayName);
        }
            
    }
}

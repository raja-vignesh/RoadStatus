using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RoadStatus;
using RoadStatus.ApiServices;
using RoadStatus.Models;
using RoadStatus.Services.ServiceInterfaces;

namespace RoadStatusTest.Features
{
    
    public class RoadStatusFeatureTests
    {
        // TEST 1
        // This test verifes that when a valid road ID ("A2") is requested,
        // the RoadStatusChecker correctly retrieve the road status from the mocked ITflApiService
        // and return the expected DisplayName in the result.
        [Fact]
        
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

        // TEST 2
        // This test verifes that when a valid road ID ("A2") is requested,
        // the RoadStatusChecker correctly retrieve the road status from the mocked ITflApiService
        // and return the expected Status in the result.

        [Fact]
        public async Task GivenValidRoadId_WhenGettingRoadStatus_ThenRoadStatusIsShown()
        {
            // Arrange
            var mockApiService = new Mock<ITflApiService>();
            var expectedRoad = new RoadCorridor
            {
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No Exceptional Delays"
            };
            mockApiService.Setup(x => x.GetRoadStatusAsync("A2"))
                         .ReturnsAsync((expectedRoad, null));

            var roadStatusChecker = new RoadStatusChecker(mockApiService.Object);

            // Act
            var result = await roadStatusChecker.CheckStatus("A2");

            // Assert
            Assert.Equal("Good", result.RoadStatus);
        }

        // TEST 3
        // This test verifes that when a valid road ID ("A2") is requested,
        // the RoadStatusChecker correctly retrieve the road status from the mocked ITflApiService
        // and return the expected Status Description in the result.
        [Fact]
        public async Task GivenValidRoadId_WhenGettingRoadStatus_ThenRoadStatusDescriptionIsShown()
        {
            // Arrange
            var mockApiService = new Mock<ITflApiService>();
            var expectedRoad = new RoadCorridor
            {
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No Exceptional Delays"
            };
            mockApiService.Setup(x => x.GetRoadStatusAsync("A2"))
                         .ReturnsAsync((expectedRoad, null));

            var roadStatusChecker = new RoadStatusChecker(mockApiService.Object);

            // Act
            var result = await roadStatusChecker.CheckStatus("A2");

            // Assert
            Assert.Equal("No Exceptional Delays", result.RoadStatusDescription);
        }

    }
}

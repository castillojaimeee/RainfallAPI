using Moq;
using NUnit.Framework;
using Sorted.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sorted.Infrastructure.Data;

namespace Sorted.UnitTest
{
    public class Tests
    {
        private Mock<RainfallReadingRepository> rainfallReadingService;
        [SetUp]
        public void Setup()
        {
            rainfallReadingService = new Mock<RainfallReadingRepository>();
        }
        [Test]
        public async Task GetRainfallReadingValidParams()
        {
            var service = rainfallReadingService.Object;
            var result = await service.GetRainfallReading("082732",5);
            Assert.IsTrue(result.Count > 0);
        }
        [Test]
        public async Task GetRainfallReadingInvalidStationId()
        {
            var service = rainfallReadingService.Object;
            var result = await service.GetRainfallReading("091111");
            Assert.IsTrue(result.Count == 0);
        }
        [Test]
        public async Task GetRainfallReadingDefaultCount()
        {
            var service = rainfallReadingService.Object;
            var result = await service.GetRainfallReading("082732");
            Assert.IsTrue(result.Count == 10);
        }
    }
}
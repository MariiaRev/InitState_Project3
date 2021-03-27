using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Client.Services;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using Xunit;

namespace PMFightAcademy.Tests.ForClient.TestControllers
{
    public class CoachesServiceTests
    {

        private Mock<ApplicationContext> _applicationContextMock;
        private ICoachesService _testedService;
        private static readonly ILogger<CoachesService> Logger = new Logger<CoachesService>(new NullLoggerFactory());


        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new CoachesService(Logger, _applicationContextMock.Object);
        }

        [Fact]
        public async Task GetServicesForBooking_Success()
        {
            Setup();

            var qualification = new Qualification()
            {
                Id = 1,
                CoachId = 1,
                ServiceId = 1,
                Service = new() { Id = 1 }
            };
            var qualifications = new List<Qualification>() { qualification };
            var expectedCoach = new Coach() { Id = 1, FirstName = "TestSlot", Description = "top serv", Qualifications = qualifications };
            var coaches = new List<Coach>() { expectedCoach };

            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);
            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new CoachesService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetCoaches(1, 1, CancellationToken.None);
            var listData = result.Data.ToList();

            Assert.Single(listData);
            Assert.Equal(1, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Fact]
        public async Task GetServicesForBooking_Fail()
        {
            Setup();

            var qualification = new Qualification()
            {
                Id = 1,
                CoachId = 1,
                ServiceId = 1,
                Service = new() { Id = 1 }
            };
            var qualifications = new List<Qualification>() { qualification };

            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(new List<Coach>());
            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new CoachesService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetCoaches(1, 1, CancellationToken.None);
            var listData = result.Data.ToList();

            Assert.Empty(listData);
        }
    }
}

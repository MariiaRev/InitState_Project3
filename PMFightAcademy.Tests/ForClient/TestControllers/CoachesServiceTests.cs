using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using PMFightAcademy.Client.Services;
using Xunit;

namespace PMFightAcademy.Tests.ForClient.TestControllers
{
    public class CoachesServiceTests
    {

        private Mock<ClientContext> _clientContextMock;
        private ICoachesService _testedService;
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ClientContext>().Options;
            _clientContextMock = new Mock<ClientContext>(options);
            _testedService = new CoachesService(_clientContextMock.Object);
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

            _clientContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);
            _clientContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new CoachesService(_clientContextMock.Object);

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

            _clientContextMock.Setup(x => x.Coaches).ReturnsDbSet(new List<Coach>());
            _clientContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new CoachesService(_clientContextMock.Object);

            var result = await _testedService.GetCoaches(1, 1, CancellationToken.None);
            var listData = result.Data.ToList();

            Assert.Empty(listData);
        }
    }
}

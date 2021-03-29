using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class QualificationServiceTests
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private IQualificationService _testedService;
        private static readonly ILogger<QualificationService> Logger = new Logger<QualificationService>(new NullLoggerFactory());


        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new QualificationService(Logger, _applicationContextMock.Object);
        }

        [Fact]
        public async Task DeleteQualification_Success()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.DeleteQualification(1, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteQualification_failed()
        {
            Setup();

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(new List<Qualification>());

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.DeleteQualification(9, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task AddQualification()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.AddQualification(new QualificationContract(), CancellationToken.None);

           Assert.True(result);
        }

        [Fact]
        public async Task GetCoachesForService_Success()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetCoachesForService(1, CancellationToken.None);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetCoachesForService_Fail()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetCoachesForService(2, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetServicesForCoach_Success()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetServicesForCoach(1, CancellationToken.None);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetServicesForCoach_Fail()
        {
            Setup();

            var expectedQual = new Qualification() { Id = 1, CoachId = 1, ServiceId = 1 };
            var qualifications = new List<Qualification>() { expectedQual };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);

            _testedService = new QualificationService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetServicesForCoach(2, CancellationToken.None);

            Assert.Empty(result);
        }
    }
}

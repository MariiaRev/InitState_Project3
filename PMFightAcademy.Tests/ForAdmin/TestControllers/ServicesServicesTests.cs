using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ServicesServicesTests
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private IServiceService _testedService;
        private static readonly ILogger<ServiceService> Logger = new Logger<ServiceService>(new NullLoggerFactory());


        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new ServiceService(Logger, _applicationContextMock.Object);
        }

        private static List<Service> GenerateListOfServices()
        {
            return new List<Service>()
            {
                new Service() {Id = 1, Name = "FirstTestService", Description = "Service one ", Price = 1111},
                new Service() {Id = 2, Name = "SecondTestService", Description = "Service two", Price = 2222},
                new Service() {Id = 3, Name = "ThirdTestService", Description = "Service three", Price = 3333},
                new Service() {Id = 4, Name = "FourthTestService", Description = "Service four", Price = 4444}
            };

        }

        [Fact]
        public async Task Take_All_Service_Check_First()
        {
            Setup();

            var services = GenerateListOfServices();
            var expectedService = services.First();

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeAllServices()).ToList();

            Assert.Equal(expectedService, result.First());

        }

        [Fact]

        public async Task Take_All_Service_Check_Count()
        {
            Setup();

            var services = GenerateListOfServices();
            var expectedCount = services.Count;

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeAllServices()).ToList();

            Assert.Equal(expectedCount, result.Count);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Take_All_Service_By_Id(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeService(id));

            Assert.Equal(services[id-1], result);

        }

        [Theory]
        [InlineData(6)]
        [InlineData(12)]
        [InlineData(66)]
        [InlineData(5)]
        public async Task Take_Coach_By_Incorrect_ID_Check(int id)
        {
            Setup();

            var services = GenerateListOfServices();


            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeService(id));

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task DeleteService_Successes(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.DeleteService(id,CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task Delete_Service_Failed(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.DeleteService(id, CancellationToken.None));

            Assert.False(result);

        }
        [Fact]
        public async Task Update_Service_Successes_One()
        {
            Setup();

            var services = GenerateListOfServices();


            var newService = new Service()
            {
                Id = 1,
                Name = "TestService",
                Description = "More Tested Service",
                Price = 1555
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.UpdateService(newService, CancellationToken.None));

            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Update_Service_Successes(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.UpdateService(services[id-1], CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public async Task Update_Service_Failed(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            var newService = new Service()
            {
                Id = id,
                Name = "TestService",
                Description = "More Tested Service",
                Price = 1555
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.UpdateService(newService, CancellationToken.None));

            Assert.False(result);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Add_Service_Failed(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            var newService = new Service()
            {
                Id = id,
                Name = "TestService",
                Description = "More Tested Service",
                Price = 1555
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.AddService(newService, CancellationToken.None));

            Assert.False(result);

        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public async Task Add_Service_Successes(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            var newService = new Service()
            {
                Id = id,
                Name = $"TestService{id}",
                Description = "More Tested Service",
                Price = 1555
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.AddService(newService, CancellationToken.None));

            Assert.True(result);

        }
    }
}
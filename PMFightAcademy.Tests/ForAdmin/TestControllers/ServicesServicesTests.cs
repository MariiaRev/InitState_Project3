using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ServicesServicesTests
    {
        private Mock<AdminContext> _adminContextMock;
        private IServiceService _testedService;
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<AdminContext>().Options;
            _adminContextMock = new Mock<AdminContext>(options);
            _testedService = new ServiceService(_adminContextMock.Object);
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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllServices()).ToList();

            Assert.Equal(expectedService, result.First());

        }

        [Fact]
        public async Task Take_All_Service_Check_Count()
        {
            Setup();

            var services = GenerateListOfServices();
            var expectedCount = services.Count;

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

            var result = (await _testedService.TakeService(id));

            Assert.Equal(services[id-1], result);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

            var result = (await _testedService.DeleteService(id,CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task DeleteService_Failed(int id)
        {
            Setup();

            var services = GenerateListOfServices();

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

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
                Name = "TestService",
                Description = "More Tested Service",
                Price = 1555
            };

            _adminContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new ServiceService(_adminContextMock.Object);

            var result = (await _testedService.AddService(newService, CancellationToken.None));

            Assert.True(result);

        }

    }
}
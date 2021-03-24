using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ServicesServicesTests
    {
        [Fact]
        public async Task FirstTest()
        {
            var expectedService = new Service() { Id = 1, Name = "TestService", Description = "top serv", Price = 5555 };

            var services = new List<Service>(){ expectedService };


            var options = new DbContextOptionsBuilder<AdminContext>()
                
                .Options;


            var serviceContextMock = new Mock<AdminContext>(options);
            serviceContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            IServiceService service = new ServiceService(serviceContextMock.Object);
            
            var actualService = await service.TakeService(1);

            Assert.Equal(expectedService, actualService);
        }
        [Fact]
        public async Task AddServiceTest()
        {

            var serviceIn = new Service() { Id = 1, Name = "TestService", Description = "top serv", Price = 5555 };

            var services = new List<Service>() { serviceIn };

            

            var options = new DbContextOptionsBuilder<AdminContext>()
                .Options;

            var serviceContextMock = new Mock<AdminContext>(options);
            serviceContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            var serviceToAdd = new Service() { Id = 2, Name = "TestService", Description = "top serv", Price = 5555 };

            IServiceService service = new ServiceService(serviceContextMock.Object);


            await service.AddService(serviceToAdd, CancellationToken.None);

            services.Add(serviceToAdd);

            var actualService = await service.TakeService(2);

            Assert.Equal(actualService, serviceToAdd);
        }


        [Fact]
        public async Task VasyaTest()
        {

            var serviceIn = new Service() { Id = 1, Name = "TestService", Description = "top serv", Price = 5555 };

            var services = new List<Service>() { serviceIn };



            var options = new DbContextOptionsBuilder<AdminContext>()
                .UseInMemoryDatabase(databaseName: "stageDb")
                .Options;

            var serviceToAdd = new Service() { Id = 2, Name = "TestService", Description = "top serv", Price = 5555 };

            var serviceContextMock = new Mock<AdminContext>(options);
            serviceContextMock.Setup(x => x.Services).ReturnsDbSet(services);


            IServiceService service = new ServiceService(serviceContextMock.Object);


            await service.AddService(serviceToAdd, CancellationToken.None);


            var actualService = await service.TakeService(2);

            Assert.Equal(actualService, serviceToAdd);
        }
    }
}
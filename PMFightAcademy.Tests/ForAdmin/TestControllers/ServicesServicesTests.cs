using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using System.Linq;
using System.Threading;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ServicesServicesTests
    {
        [Fact]

        public async Task FirstTest()
        {
            var services = new List<Service>();
            var serviceContextMock = new Mock<AdminContext>();
            serviceContextMock.Setup(x => x.Set<Service>()).ReturnsDbSet(services);
            var serviceToAdd = new Service() { Id = 0, Name = "TestService", Description = "top serv", Price = 5555 };
            IServiceService service = new ServiceService(serviceContextMock.Object);

            await service.AddService(serviceToAdd,CancellationToken.None);
            var serv = services.FirstOrDefault();
            Assert.Equal(serv , serviceToAdd);
        }
    }
}
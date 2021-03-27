using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Services;
using PMFightAcademy.Dal.DataBase;
using Xunit;

namespace PMFightAcademy.Tests.ForClient.TestControllers
{
    public class ClientServiceTests
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private IClientsService _testedService;
        private static readonly ILogger<ClientsService> Logger = new Logger<ClientsService>(new LoggerFactory());
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new ClientsService(Logger, _applicationContextMock.Object);
        }

        [Theory]
        [InlineData("+380671234567", "String88")]
        [InlineData("380671234567", "String88")]
        [InlineData("0671234567", "String88")]
        public async Task Register_Fail(string phone, string password)
        {
            Setup();

            var listClients = new List<Dal.Models.Client>()
            {
                new()
                {
                    Login = "0671234567"
                }
            };

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _applicationContextMock.Object);

            var model = new ClientDto()
            {
                Login = phone,
                Password = password,
                Name = "Maga"
            };

            var result = await _testedService.Register(model, CancellationToken.None);

            Assert.Empty(result);
        }


        [Theory]
        [InlineData("+380671234567", "String88")]
        [InlineData("380671234567", "String88")]
        [InlineData("0671234567", "String88")]
        public async Task Login_Success(string phone, string password)
        {
            Setup();

            var listClients = new List<Dal.Models.Client>()
            {
                new()
                {
                    Id = 0,
                    Login = "0671234567",
                    Password = "Ouxp11wblXlj4V33Gr/Xi2+S6GJ1C3p1luDmPWJkh2Q="
                }
            };

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _applicationContextMock.Object);

            var model = new LoginContract()
            {
                Login = phone,
                Password = password
            };

            var result = await _testedService.Login(model, CancellationToken.None);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("+380671234567", "String888")]
        [InlineData("380691234567", "String888")]
        [InlineData("0691234567", "String88")]
        public async Task Login_Fail(string phone, string password)
        {
            Setup();

            var listClients = new List<Dal.Models.Client>()
            {
                new()
                {
                    Id = 0,
                    Login = "0671234567",
                    Password = "Ouxp11wblXlj4V33Gr/Xi2+S6GJ1C3p1luDmPWJkh2Q="
                }
            };

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _applicationContextMock.Object);

            var model = new LoginContract()
            {
                Login = phone,
                Password = password
            };

            var result = await _testedService.Login(model, CancellationToken.None);

            Assert.Empty(result);
        }
    }
}

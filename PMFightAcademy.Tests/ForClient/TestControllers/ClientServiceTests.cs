using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Services;
using Xunit;

namespace PMFightAcademy.Tests.ForClient.TestControllers
{
    public class ClientServiceTests
    {
        private Mock<ClientContext> _clientContextMock;
        private IClientsService _testedService;
        private static readonly ILogger<ClientsService> Logger = new Logger<ClientsService>(new LoggerFactory());
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ClientContext>().Options;
            _clientContextMock = new Mock<ClientContext>(options);
            _testedService = new ClientsService(Logger, _clientContextMock.Object);
        }

        [Theory]
        [InlineData("+380671234567", "String88")]
        [InlineData("380671234567", "String88")]
        [InlineData("0671234567", "String88")]
        [InlineData("0661234567", "String88")]
        public async Task Register_Success(string phone, string password)
        {
            Setup();

            _clientContextMock.Setup(x => x.Clients).ReturnsDbSet(new List<Client.Models.Client>());

            _testedService = new ClientsService(Logger, _clientContextMock.Object);

            var model = new Client.Models.Client()
            {
                Login = phone,
                Password = password,
                Name = "Maga"
            };

            var result = await _testedService.Register(model, CancellationToken.None);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("+380671234567", "String88")]
        [InlineData("380671234567", "String88")]
        [InlineData("0671234567", "String88")]
        public async Task Register_Fail(string phone, string password)
        {
            Setup();

            var listClients = new List<Client.Models.Client>()
            {
                new()
                {
                    Login = "0671234567"
                }
            };

            _clientContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _clientContextMock.Object);

            var model = new Client.Models.Client()
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

            var listClients = new List<Client.Models.Client>()
            {
                new()
                {
                    Id = 0,
                    Login = "0671234567",
                    Password = "Ouxp11wblXlj4V33Gr/Xi2+S6GJ1C3p1luDmPWJkh2Q="
                }
            };

            _clientContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _clientContextMock.Object);

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

            var listClients = new List<Client.Models.Client>()
            {
                new()
                {
                    Id = 0,
                    Login = "0671234567",
                    Password = "Ouxp11wblXlj4V33Gr/Xi2+S6GJ1C3p1luDmPWJkh2Q="
                }
            };

            _clientContextMock.Setup(x => x.Clients).ReturnsDbSet(listClients);

            _testedService = new ClientsService(Logger, _clientContextMock.Object);

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

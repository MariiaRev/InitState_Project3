using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using PMFightAcademy.Dal.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ClientServiceTest
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private IClientService _testedService;
        private static readonly ILogger<ClientService> Logger = new Logger<ClientService>(new NullLoggerFactory());

        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new ClientService(Logger, _applicationContextMock.Object);
        }

        private static List<ClientContract> GenerateListOfClientsContracts()
        {
            return new List<ClientContract>()
            {
                new () {Id = 1, Description = "Client one ", Login= "+380145674584",Name = "Adam"},
                new () {Id = 2, Description = "Client two ", Login= "+380125670584",Name = "Mark"},
                new () {Id = 3, Description = "Client three ", Login= "+380146674580",Name = "Leo"},
                new () {Id = 4, Description = "Client four ", Login= "+380125674594",Name = "Holland"}
            };

        }

        private static List<Dal.Models.Client> GenerateListOfClients()
        {
            return new List<Dal.Models.Client>()
            {
                new () {Id = 1, Description = "Client one ", Login= "+380145674584",Name = "Adam"},
                new () {Id = 2, Description = "Client two ", Login= "+380125670584",Name = "Mark"},
                new () {Id = 3, Description = "Client three ", Login= "+380146674580",Name = "Leo"},
                new () {Id = 4, Description = "Client four ", Login= "+380125674594",Name = "Holland"}
            };

        }
        [Fact]
        public async Task Take_All_Clients()
        {
            Setup();

            var clients = GenerateListOfClients();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);
            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeAllClients()).ToList();

            Assert.Equal(result.Count, clients.Count);
        }

        [Fact]
        public async Task Take_First_Client()
        {
            Setup();

            var clients = GenerateListOfClients();

            var clientsContact = GenerateListOfClientsContracts();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeAllClients()).FirstOrDefault();

            var client = clientsContact.FirstOrDefault();

            Assert.Equal(client.Description, result.Description);
            Assert.Equal(client.Name, result.Name);
            Assert.Equal(client.Id, result.Id);
            Assert.Equal(client.Login, result.Login);
        }

        [Fact]
        public async Task Take_Count_Clients()
        {
            Setup();

            var clients = GenerateListOfClients();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeAllClients()).Count() ;

            Assert.Equal(clients.Count, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Take_Client_With_Correct_Id(int id)
        {
            Setup();

            var clients = GenerateListOfClients();

            var clientsContact = GenerateListOfClientsContracts();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeClient(id));

            Assert.Equal(clientsContact[id-1].Description, result.Description);
            Assert.Equal(clientsContact[id - 1].Id, result.Id);
            Assert.Equal(clientsContact[id - 1].Name, result.Name);
            Assert.Equal(clientsContact[id - 1].Login, result.Login);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Take_Client_With_Incorrect_Id(int id)
        {
            Setup();

            var clients = GenerateListOfClients();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.TakeClient(id));

            Assert.Null(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Update_Description_Failed_Id(int id)
        {
            Setup();

            var clients = GenerateListOfClients();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.AddDescription(id,"AnyDisc",CancellationToken.None));

            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Update_Description_Successes(int id)
        {
            Setup();

            var clients = GenerateListOfClients();

            _applicationContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.AddDescription(id, "AnyDisc", CancellationToken.None));

            Assert.True(result);
        }
    }
}
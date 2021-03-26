using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class ClientServiceTest
    {
        private Mock<AdminContext> _adminContextMock;
        private IClientService _testedService;
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<AdminContext>().Options;
            _adminContextMock = new Mock<AdminContext>(options);
            _testedService = new ClientService(_adminContextMock.Object);
        }

        private static List<Admin.Models.Client> GenerateListOfClients()
        {
            return new List<Admin.Models.Client>()
            {
                new Admin.Models.Client() {Id = 1, Description = "Client one ", Login= "+380145674584"},
                new Admin.Models.Client() {Id = 2, Description = "Client two ", Login= "+380125670584"},
                new Admin.Models.Client() {Id = 3, Description = "Client three ", Login= "+380146674580"},
                new Admin.Models.Client() {Id = 4, Description = "Client four ", Login= "+380125674594"}
            };

        }

        [Fact]
        public async Task Take_All_Clients()
        {
            Setup();

            var clients = GenerateListOfClients();

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllClients()).ToList();

            Assert.Equal(clients,result);
        }

        [Fact]
        public async Task Take_First_Client()
        {
            Setup();

            var clients = GenerateListOfClients();

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllClients()).FirstOrDefault();

            Assert.Equal(clients.FirstOrDefault(), result);
        }

        [Fact]
        public async Task Take_Count_Clients()
        {
            Setup();

            var clients = GenerateListOfClients();

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

            var result = (await _testedService.TakeClient(id));

            Assert.Equal(clients[id-1], result);
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

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

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

            _adminContextMock.Setup(x => x.Clients).ReturnsDbSet(clients);

            _testedService = new ClientService(_adminContextMock.Object);

            var result = (await _testedService.AddDescription(id, "AnyDisc", CancellationToken.None));

            Assert.True(result);
        }
    }
}
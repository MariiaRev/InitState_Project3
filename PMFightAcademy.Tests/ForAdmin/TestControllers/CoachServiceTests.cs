using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class CoachServiceTests
    {

        private Mock<AdminContext> _adminContextMock;
        private ICoachService _testedService;
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<AdminContext>().Options;
            _adminContextMock = new Mock<AdminContext>(options);
            _testedService = new CoachService(_adminContextMock.Object);
        }

        private static List<Coach> GenerateListOfCoches()
        {
            return new List<Coach>()
            {
                new Coach() {Id = 1,FirstName = "Ivan",LastName = "Ivanov", Description = "Service one ", BirthDate = new DateTime(2001,01,01),PhoneNumber="+380145674584"},
                new Coach() {Id = 2,FirstName = "Andrew",LastName = "Anrewech", Description = "Service one ", BirthDate = new DateTime(2002,02,02),PhoneNumber="+380145674584"},
                new Coach() {Id = 3,FirstName = "Bohdan",LastName = "Bohdanovich", Description = "Service one ", BirthDate = new DateTime(2003,03,03),PhoneNumber="+380145674584"},
                new Coach() {Id = 4,FirstName = "Alex",LastName = "Alexanov", Description = "Service one ", BirthDate = new DateTime(2004,04,04),PhoneNumber="+380145674584"},
            };

        }
        private static List<CoachContract> GenerateListOfCoachesContract()
        {
            return new List<CoachContract>()
            {
                new CoachContract() {Id = 1,FirstName = "Ivan",LastName = "Ivanov", Description = "Service one ", DateBirth = "01.01.2001" ,PhoneNumber="+380145674584"},
                new CoachContract() {Id = 2,FirstName = "Andrew",LastName = "Anrewech", Description = "Service one ", DateBirth = "02.02.2002" ,PhoneNumber="+380145674584"},
                new CoachContract() {Id = 3,FirstName = "Bohdan",LastName = "Bohdanovich", Description = "Service one ", DateBirth = "03.03.2003" ,PhoneNumber="+380145674584"},
                new CoachContract() {Id = 4,FirstName = "Alex",LastName = "Alexanov", Description = "Service one ", DateBirth = "04.04.2004" ,PhoneNumber="+380145674584"}
            };

        }
        [Fact]
        public async Task Take_All_Coaches_First_Successes()
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var coachesContract = GenerateListOfCoachesContract();
            

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllCoaches()).ToList();

            Assert.Equal(result.First().DateBirth, coachesContract.First().DateBirth);
            Assert.Equal(result.First().FirstName, coachesContract.First().FirstName);
            Assert.Equal(result.First().LastName, coachesContract.First().LastName);
            Assert.Equal(result.First().PhoneNumber, coachesContract.First().PhoneNumber);
            Assert.Equal(result.First().Id, coachesContract.First().Id);
            Assert.Equal(result.First().Description, coachesContract.First().Description);

        }

        [Fact]
        public async Task Take_All_Service_Check_Last_Successes()
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var coachesContract = GenerateListOfCoachesContract();

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllCoaches()).ToList();

            Assert.Equal(result.Last().DateBirth, coachesContract.Last().DateBirth);
            Assert.Equal(result.Last().FirstName, coachesContract.Last().FirstName);
            Assert.Equal(result.Last().LastName, coachesContract.Last().LastName);
            Assert.Equal(result.Last().PhoneNumber, coachesContract.Last().PhoneNumber);
            Assert.Equal(result.Last().Id, coachesContract.Last().Id);
            Assert.Equal(result.Last().Description, coachesContract.Last().Description);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Take_Coach_By_ID_Check(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var coachesContract = GenerateListOfCoachesContract();

            var expectedCoach = coachesContract[id - 1];

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.TakeCoach(id));

            Assert.Equal(expectedCoach.DateBirth, result.DateBirth);
            Assert.Equal(expectedCoach.FirstName, result.FirstName);
            Assert.Equal(expectedCoach.LastName, result.LastName);
            Assert.Equal(expectedCoach.PhoneNumber, result.PhoneNumber);
            Assert.Equal(expectedCoach.Id, result.Id);
            Assert.Equal(expectedCoach.Description, result.Description);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(12)]
        [InlineData(66)]
        [InlineData(5)]
        public async Task Take_Coach_By_Incorrect_ID_Check(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var coachesContract = GenerateListOfCoachesContract();

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.TakeCoach(id));
            
            Assert.Null(result);
        }

        [Fact]
        public async Task Take_All_Coaches_Count_Successes()
        {
            Setup();

            var coaches = GenerateListOfCoches();


            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.TakeAllCoaches()).ToList();

            Assert.Equal(result.Count,coaches.Count);
        }

        [Fact]
        public async Task Add_Coach()
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = "01.02.2000", Description = "TestDesc", FirstName = "Denis", LastName = "Denisovich",
                Id = 0, PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.AddCoach(newCoach,CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData("01.02.2011")]
        [InlineData("01.02.2009")]
        [InlineData("01.02.1500")]
        [InlineData("01.02.1920")]
        public async Task Add_Coach_Failed_Age(string dateBirth)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = dateBirth,
                Description = "TestDesc",
                FirstName = "Denis",
                LastName = "Denisovich",
                Id = 0,
                PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.AddCoach(newCoach, CancellationToken.None));

            Assert.False(result);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Add_Coach_Failed_Id(int id )
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = "01.02.1999",
                Description = "TestDesc",
                FirstName = "Denis",
                LastName = "Denisovich",
                Id = id,
                PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.AddCoach(newCoach, CancellationToken.None));

            Assert.False(result);

        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Add_Coach_Correct_Id(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = "01.02.1999",
                Description = "TestDesc",
                FirstName = "Denis",
                LastName = "Denisovich",
                Id = id,
                PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.AddCoach(newCoach, CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Update_Failed_Incorrect_id(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = "01.02.1999",
                Description = "TestDesc",
                FirstName = "Denis",
                LastName = "Denisovich",
                Id = id,
                PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.UpdateCoach(newCoach, CancellationToken.None));

            Assert.False(result);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Update_Failed_Correct_id(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();

            var newCoach = new CoachContract()
            {
                DateBirth = "01.02.1999",
                Description = "TestDesc",
                FirstName = "Denis",
                LastName = "Denisovich",
                Id = id,
                PhoneNumber = "+380145674584"
            };

            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.UpdateCoach(newCoach, CancellationToken.None));

            Assert.True(result);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Delete_Failed_Correct_Id(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();


            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.DeleteCoach(id, CancellationToken.None));

            Assert.True(result);

        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Delete_Failed_Incorrect_Id(int id)
        {
            Setup();

            var coaches = GenerateListOfCoches();


            _adminContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new CoachService(_adminContextMock.Object);

            var result = (await _testedService.DeleteCoach(id, CancellationToken.None));

            Assert.False(result);

        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SlotServiceTest
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private ISlotService _testedService;
        private static readonly ILogger<SlotService> Logger = new Logger<SlotService>(new NullLoggerFactory());

        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new SlotService(Logger, _applicationContextMock.Object);
        }

        private static IEnumerable<Slot> GenerateListOfSlots()
        {
            return new List<Slot>()
            {
                new Slot() {Id = 1,CoachId = 1,Date = new DateTime(2022,01,01),StartTime = new TimeSpan(0,10,0,0)},
                new Slot() {Id = 2,CoachId = 2,Date = new DateTime(2022,02,02),StartTime = new TimeSpan(0,11,0,0)},
                new Slot() {Id = 3,CoachId = 3,Date = new DateTime(2022,03,03),StartTime = new TimeSpan(0,12,0,0)},
                new Slot() {Id = 4,CoachId = 4,Date = new DateTime(2022,04,04),StartTime = new TimeSpan(0,13,0,0)},
            };
        }
        private static IEnumerable<SlotsReturnContract> GenerateListOfSlotsContract()
        {
            return new List<SlotsReturnContract>()
            {
                new SlotsReturnContract() {Id = 1,CoachId = 1,DateStart = "01.01.2022",TimeStart = "10:00"},
                new SlotsReturnContract() {Id = 2,CoachId = 2,DateStart = "02.02.2022",TimeStart = "11:00"},
                new SlotsReturnContract() {Id = 3,CoachId = 3,DateStart = "03.03.2022",TimeStart = "12:00"},
                new SlotsReturnContract() {Id = 4,CoachId = 4,DateStart = "04.04.2022",TimeStart = "13:00"},
            };
            
        }

        [Fact]
        public async Task Get_All_Slots_Successes()
        {
            Setup();
            var slots = GenerateListOfSlots();

            var slotsContract = GenerateListOfSlotsContract();

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            

            var result = await _testedService.TakeAllSlots();

            var actual = result.ToList();
            var i = 0;

            
            foreach (var expectedContract in slotsContract)
            {
                var dur = expectedContract.Duration;
                Assert.Equal(expectedContract.Id, actual[i].Id);
                Assert.Equal(expectedContract.CoachId, actual[i].CoachId);
                Assert.Equal(expectedContract.TimeStart, actual[i].TimeStart);
                Assert.Equal(expectedContract.DateStart, actual[i].DateStart);
                i++;
            }
        }

        [Fact]
        public async Task Get_Count_Slots_Successes()
        {
            Setup();
            var slots = GenerateListOfSlots();

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.TakeAllSlots();

            Assert.Equal(slots.Count(),result.Count());
        }

        [Fact]
        public async Task Create_Slots_Successes()
        {
            Setup();
            var slots = GenerateListOfSlots();

            var firstSlot = new SlotsReturnContract()
            {
                CoachId = 5,
                DateStart = "02.20.2001",
                TimeStart = "11:00"
            };
            var SecondSlot = new SlotsReturnContract()
            {
                CoachId = 5,
                DateStart = "02.20.2001",
                TimeStart = "10:00"
            };

            List<SlotsReturnContract> slotsAdd = new List<SlotsReturnContract>(){firstSlot,SecondSlot};

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.AddListOfSlots(slotsAdd,CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Create_Slots_Failed()
        {
            Setup();
            var slots = GenerateListOfSlots();

            var EmptyList = new List<SlotsReturnContract>();

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.AddListOfSlots(EmptyList, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task Slots_Remove_Failed_Null()
        {
            Setup();
            var slots = GenerateListOfSlots();

            var EmptyList = new List<int>();

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.RemoveSlotRange(EmptyList, CancellationToken.None);

            Assert.False(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task Slots_Remove_Successes(int id)
        {
            Setup();
            var slots = GenerateListOfSlots();

            var ByOneList = new List<int>(){id};

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.RemoveSlotRange(ByOneList, CancellationToken.None);

            Assert.True(result);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(7)]
        public async Task Slots_Remove_Failed(int id)
        {
            Setup();
            var slots = GenerateListOfSlots();

            //var ByOneList = new List<int>() { id };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(Logger,_applicationContextMock.Object);

            var result = await _testedService.RemoveSlot(id, CancellationToken.None);

            Assert.False(result);
        }
    }
}
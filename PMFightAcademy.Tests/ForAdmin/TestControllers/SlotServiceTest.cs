using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new SlotService(_applicationContextMock.Object);
        }

        private static IEnumerable<Slot> GenerateListOfSlots()
        {
            return new List<Slot>()
            {
                new Slot() {Id = 1,CoachId = 1,Date = new DateTime(2001,01,01),StartTime = new TimeSpan(0,10,0,0)},
                new Slot() {Id = 2,CoachId = 2,Date = new DateTime(2002,02,02),StartTime = new TimeSpan(0,11,0,0)},
                new Slot() {Id = 3,CoachId = 3,Date = new DateTime(2003,03,03),StartTime = new TimeSpan(0,12,0,0)},
                new Slot() {Id = 4,CoachId = 4,Date = new DateTime(2004,04,04),StartTime = new TimeSpan(0,13,0,0)},
            };
        }
        private static IEnumerable<SlotsReturnContract> GenerateListOfSlotsContract()
        {
            return new List<SlotsReturnContract>()
            {
                new SlotsReturnContract() {Id = 1,CoachId = 1,DateStart = "01.01.2001",TimeStart = "10:00"},
                new SlotsReturnContract() {Id = 2,CoachId = 2,DateStart = "02.02.2002",TimeStart = "11:00"},
                new SlotsReturnContract() {Id = 3,CoachId = 3,DateStart = "03.03.2003",TimeStart = "12:00"},
                new SlotsReturnContract() {Id = 4,CoachId = 4,DateStart = "04.04.2004",TimeStart = "13:00"},
            };
            
        }

        [Fact]
        public async Task Get_All_Slots_Successes()
        {
            Setup();
            var slots = GenerateListOfSlots();

            var slotsContract = GenerateListOfSlotsContract();

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);

            _testedService = new SlotService(_applicationContextMock.Object);

            

            var result = await _testedService.TakeAllSlots();

            var actual = result.ToList();
            var i = 0;

            
            foreach (var expectedContract in slotsContract)
            {
                var dur = expectedContract.Duration;
                Assert.Equal(expectedContract.Id, actual[i].Id);
                Assert.Equal(expectedContract.CoachId, actual[i].CoachId);
                Assert.Equal(expectedContract.Duration, actual[i].Duration);
                Assert.Equal(expectedContract.TimeStart, actual[i].TimeStart);
                Assert.Equal(expectedContract.DateStart, actual[i].DateStart);
                i++;
            }
            

        }
    }
}
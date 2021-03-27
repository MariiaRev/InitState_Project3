using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class SlotServiceTest
    {
        private Mock<AdminContext> _adminContextMock;
        private ISlotService _testedService;
        private void Setup()
        {
            var options = new DbContextOptionsBuilder<AdminContext>().Options;
            _adminContextMock = new Mock<AdminContext>(options);
            _testedService = new SlotService(_adminContextMock.Object);
        }

        private static List<Slot> GenerateListOfSlots()
        {
            return new List<Slot>()
            {
                new Slot() {Id = 1,CoachId = 1,Date = new DateTime(2001,01,01),StartTime = new TimeSpan(0,10,0,0)},
                new Slot() {Id = 2,CoachId = 2,Date = new DateTime(2002,02,02),StartTime = new TimeSpan(0,11,0,0)},
                new Slot() {Id = 3,CoachId = 3,Date = new DateTime(2003,03,03),StartTime = new TimeSpan(0,12,0,0)},
                new Slot() {Id = 4,CoachId = 4,Date = new DateTime(2004,04,04),StartTime = new TimeSpan(0,13,0,0)},
            };
        }
        private static List<SlotsReturnContract> GenerateListOfSlotsContract()
        {
            return new List<SlotsReturnContract>()
            {
                new SlotsReturnContract() {Id = 1,CoachId = 1,DateStart = "01.01.2001",TimeStart = "10:00"},
                new SlotsReturnContract() {Id = 2,CoachId = 2,DateStart = "02.02.2002",TimeStart = "11:00"},
                new SlotsReturnContract() {Id = 3,CoachId = 3,DateStart = "03.03.2003",TimeStart = "12:00"},
                new SlotsReturnContract() {Id = 4,CoachId = 4,DateStart = "04.04.2004",TimeStart = "13:00"},
            };
            
        }

        //Vlad ia ce vze zavtra dopyshy , a to duz spaty hochy)
    }
}
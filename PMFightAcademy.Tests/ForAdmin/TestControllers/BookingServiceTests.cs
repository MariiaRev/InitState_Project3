﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using Xunit;

namespace PMFightAcademy.Tests.ForAdmin.TestControllers
{
    public class BookingServiceTests
    {
        private Mock<ApplicationContext> _applicationContextMock;
        private IBookingService _testedService;
        private static readonly ILogger<BookingService> Logger = new Logger<BookingService>(new NullLoggerFactory()); 

        private void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().Options;
            _applicationContextMock = new Mock<ApplicationContext>(options);
            _testedService = new BookingService(Logger, _applicationContextMock.Object);
        }

        [Fact]
        public async Task TakeAllBooking()
        {
            Setup();

            var bookings = new List<Booking>() { new () };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.TakeAllBooking(CancellationToken.None);

            Assert.Single(result);
        }

        [Fact]
        public async Task TakeBookingForCoach_Success()
        {
            Setup();
            var slot = new Slot() {Id = 1, CoachId = 1};
            var bookings = new List<Booking>() { new() {Id = 1, Slot = slot } };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.TakeBookingForCoach(1, CancellationToken.None);

            Assert.Single(result);
        }

        [Fact]
        public async Task TakeBookingForCoach_Fail()
        {
            Setup();
            var slot = new Slot() { Id = 1, CoachId = 2 };
            var bookings = new List<Booking>() { new() { Id = 1, Slot = slot } };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.TakeBookingForCoach(1, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task TakeBookingOnClient_Success()
        {
            Setup();
            var bookings = new List<Booking>()
            {
                new() { Id = 1, ClientId = 1},
                new() { Id = 2, ClientId = 2},
                new() { Id = 3, ClientId = 1}
            };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.TakeBookingOnClient(1, CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task TakeBookingOnClient_Fail()
        {
            Setup();
            var bookings = new List<Booking>()
            {
                new() { Id = 1, ClientId = 1},
                new() { Id = 2, ClientId = 2},
                new() { Id = 3, ClientId = 1}
            };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.TakeBookingOnClient(3, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateBooking()
        {
            Setup();
            var bookings = new List<Booking>()
            {
                new() { Id = 1, ClientId = 1},
                new() { Id = 2, ClientId = 2},
                new() { Id = 3, ClientId = 1}
            };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var bookingDto = new BookingUpdateContract()
            {
                Id = 1,
                ClientId = 1,
                ResultPrice = 500,
                ServiceId = 1,
            };

            var result = await _testedService.UpdateBooking(bookingDto, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveBooking_Success()
        {
            Setup();
            var bookings = new List<Booking>()
            {
                new() { Id = 1, ClientId = 1},
                new() { Id = 2, ClientId = 2},
                new() { Id = 3, ClientId = 1}
            };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.RemoveBooking(1, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveBooking_Fail()
        {
            Setup();

            var bookings = new List<Booking>()
            {
                new() { Id = 1, ClientId = 1},
                new() { Id = 2, ClientId = 2},
                new() { Id = 3, ClientId = 1}
            };

            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.RemoveBooking(5, CancellationToken.None);

            Assert.False(result);
        }

    }
}

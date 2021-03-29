using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using PMFightAcademy.Client.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PMFightAcademy.Client.Contract.Dto;
using Xunit;
using BookingService = PMFightAcademy.Client.Services.BookingService;

namespace PMFightAcademy.Tests.ForClient.TestControllers
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
        public async Task GetServicesForBooking()
        {
            Setup();

            var expectedService = new Service() { Id = 1, Name = "TestService", Description = "top serv", Price = 5555 };
            var services = new List<Service>() { expectedService };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetServicesForBooking(CancellationToken.None)).ToArray();

            Assert.Single(result);
            Assert.Equal(expectedService, result.First());
        }

        [Fact]
        public async Task GetServicesForBookingWithPagination()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetServicesForBooking(3, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Equal(3, listData.Length);
            Assert.Equal(services.First(), listData.First());
            Assert.Equal(1, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Theory]
        [InlineData(3, 2)]
        [InlineData(-3, -1)]
        public async Task GetServicesForBookingWithPagination_Fail(int pageSize, int page)
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = await _testedService.GetServicesForBooking(pageSize, page, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Empty(listData);
        }

        [Fact]
        public async Task GetCoachesForBooking()
        {
            Setup();

            var services = new List<Service>() { new() { Id = 1, Name = "TestService", Description = "Description", Price = 5555 } };
            var coaches = new List<Coach>() { new() { Id = 1, FirstName = "Oleg" } };
            var qualifications = new List<Qualification>() { new() { Id = 1, CoachId = 1, ServiceId = 1, Service = services.First(), Coach = coaches.First() } };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetCoachesForBooking(1, CancellationToken.None)).ToArray();

            Assert.Single(result);
            Assert.Equal("Oleg", result.First().FirstName);
        }

        [Fact]
        public async Task GetCoachesForBooking_UnsuitableService()
        {
            Setup();

            var searchCoach = new Coach() { Id = 1, FirstName = "Oleg" };
            var searchService = new Service() { Id = 2, Name = "TestService", Description = "Description", Price = 5555 };
            var services = new List<Service>() { searchService };

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 2, Coach = searchCoach, Service = searchService}
            };

            var coaches = new List<Coach>()
            {
                searchCoach,
                new() { Id = 2, FirstName = "Artem" }
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetCoachesForBooking(1, CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCoachesForBooking_UnsuitableQualification()
        {
            Setup();

            var services = new List<Service>() { new() { Id = 1, Name = "TestService", Description = "Description", Price = 5555 } };
            var qualifications = new List<Qualification>() { new() { Id = 1, CoachId = 1, ServiceId = 2 } };
            var coaches = new List<Coach>() { new() { Id = 1, FirstName = "Oleg" } };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetCoachesForBooking(1, CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCoachesForBookingWithPagination()
        {
            Setup();
            var searchCoach = new Coach() { Id = 1, FirstName = "Oleg" };
            var searchService = new Service() { Id = 1, Name = "TestService", Description = "Description", Price = 5555 };

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1, Coach = searchCoach, Service = searchService},
                new() { Id = 2, CoachId = 2, ServiceId = 2, Service = searchService }
            };
            var coaches = new List<Coach>()
            {
                searchCoach,
                new() { Id = 2, FirstName = "Artem" }
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetCoachesForBooking(1, 3, 1, CancellationToken.None));

            var listCoach = result.Data.ToArray();

            Assert.Single(listCoach);
            Assert.Equal("Oleg", listCoach.First().FirstName);
            Assert.Equal(1, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Fact]
        public async Task GetCoachesForBookingWithPagination_UnsuitableQualification()
        {
            Setup();
            var searchCoach = new Coach() { Id = 2, FirstName = "Oleg" };
            var searchService = new Service() { Id = 2, Name = "TestService", Description = "Description", Price = 5555 };

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 2, ServiceId = 3, Coach = searchCoach, Service = searchService},
                new() { Id = 2, CoachId = 1, ServiceId = 2, Service = searchService }
            };
            var coaches = new List<Coach>()
            {
                searchCoach,
                new() { Id = 2, FirstName = "Artem" }
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Coaches).ReturnsDbSet(coaches);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetCoachesForBooking(1, 3, 1, CancellationToken.None));

            var listCoach = result.Data.ToArray();

            Assert.Empty(listCoach);
            Assert.Equal(0, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Fact]
        public async Task GetDatesForBooking()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1, CoachId = 3, Date =new DateTime(2021, 6, 16)},
                new() { Id = 2, CoachId = 2, Date =new DateTime(2021, 4, 20)}
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetDatesForBooking(2, 3, CancellationToken.None)).ToArray();

            Assert.Single(result);
            Assert.Equal("06.16.2021", result.First());
        }

        [Fact]
        public async Task GetDatesForBooking_UnsuitableQualification()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 3 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1, CoachId = 3, Date =new DateTime(2021, 6, 16)},
                new() { Id = 2, CoachId = 2, Date =new DateTime(2021, 4, 20)}
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetDatesForBooking(2, 3, CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDatesForBooking_UnsuitableSlot()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1, CoachId = 4, Date =new DateTime(2021, 6, 16)},
                new() { Id = 2, CoachId = 2, Date =new DateTime(2021, 4, 20)}
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetDatesForBooking(2, 3, CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetDatesForBooking_UnsuitableBooking()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1, CoachId = 3, Date =new DateTime(2021, 6, 16)},
                new() { Id = 2, CoachId = 2, Date =new DateTime(2021, 4, 20)}
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 1, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetDatesForBooking(2, 3, CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTimeSlotsForBooking()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetTimeSlotsForBooking(2, 3, "06.16.2021", CancellationToken.None)).ToArray();

            Assert.Single(result);
            Assert.Equal("23:00", result.First());
        }

        [Fact]
        public async Task GetTimeSlotsForBooking_UnsuitableQualification()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 1 },
                new() { Id = 3, CoachId = 3, ServiceId = 3 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetTimeSlotsForBooking(2, 3, "06.16.2021", CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTimeSlotsForBooking_UnsuitableSlot()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 7, 17),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetTimeSlotsForBooking(2, 3, "06.16.2021", CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTimeSlotsForBooking_UnsuitableBooking()
        {
            Setup();

            var qualifications = new List<Qualification>() {
                new() { Id = 1, CoachId = 1, ServiceId = 1},
                new() { Id = 2, CoachId = 2, ServiceId = 2 },
                new() { Id = 3, CoachId = 3, ServiceId = 2 }
            };
            var slots = new List<Slot>()
            {
                new() { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 1, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Qualifications).ReturnsDbSet(qualifications);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result = (await _testedService.GetTimeSlotsForBooking(2, 3, "06.16.2021", CancellationToken.None)).ToArray();

            Assert.Empty(result);
        }

        [Fact]
        public async Task AddBooking()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "06.16.2021",
                Time = "23:00",
                CoachId = 3,
                ServiceId = 3
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task AddBooking_UnsuitableSlot()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 59, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(19, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "06.16.2021",
                Time = "23:00",
                CoachId = 3,
                ServiceId = 3
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task AddBooking_UnsuitableBooking()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 1, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "06.16.2021",
                Time = "23:00",
                CoachId = 3,
                ServiceId = 3
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task AddBooking_UnsuitableService()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "06.16.2021",
                Time = "23:00",
                CoachId = 3,
                ServiceId = 88
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task AddBooking_UnsuitableDate()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "07.17.2021",
                Time = "23:00",
                CoachId = 3,
                ServiceId = 3
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task AddBooking_UnsuitableTime()
        {
            Setup();

            var services = new List<Service>()
            {
                new(){ Id = 1, Name = "FirstService", Description = "Description", Price = 5555 },
                new(){ Id = 2, Name = "SecondService", Description = "Description ", Price = 6666 },
                new(){ Id = 3, Name = "ThirdService", Description = "Description", Price = 7777}
            };
            var slots = new List<Slot>()
            {
                new()
                { Id = 1,
                    CoachId = 3,
                    Date =new DateTime(2021, 6, 16),
                    StartTime = new TimeSpan(23, 0, 0)
                },
                new()
                {
                    Id = 2,
                    CoachId = 2,
                    Date =new DateTime(2021, 4, 20),
                    StartTime = new TimeSpan(18, 0, 0)
                }
            };
            var bookings = new List<Booking>()
            {
                new() { Id = 1, SlotId = 2, ServiceId = 3},
                new() { Id = 2, SlotId = 5, ServiceId = 2}
            };

            _applicationContextMock.Setup(x => x.Services).ReturnsDbSet(services);
            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var dto = new BookingDto()
            {
                Date = "06.16.2021",
                Time = "23:59",
                CoachId = 3,
                ServiceId = 3
            };

            var result = await _testedService.AddBooking(dto, 1, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task GetActiveBookings()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2021, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1, 
                    SlotId = 1,
                    Slot = firstSlot,
                    ClientId = 1, 
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetActiveBookings(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Single(listData);
            Assert.Equal("Maga", listData.First().CoachFirstName);
            Assert.Equal(1, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Fact]
        public async Task GetActiveBookings_ExpiredSlots()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2020, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1,
                    SlotId = 1,
                    Slot = firstSlot,
                    ClientId = 1,
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetActiveBookings(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Empty(listData);
        }

        [Fact]
        public async Task GetActiveBookings_UnsuitableBooking()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2021, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1,
                    SlotId = 2,
                    Slot = firstSlot,
                    ClientId = 2,
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetActiveBookings(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Empty(listData);
        }

        [Fact]
        public async Task GetBookingHistoryBookings()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2020, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1,
                    SlotId = 1,
                    Slot = firstSlot,
                    ClientId = 1,
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetBookingHistory(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Single(listData);
            Assert.Equal("Maga", listData.First().CoachFirstName);
            Assert.Equal(1, result.Paggination.TotalPages);
            Assert.Equal(1, result.Paggination.Page);
        }

        [Fact]
        public async Task GetBookingHistory_NewSlots()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2021, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1,
                    SlotId = 1,
                    Slot = firstSlot,
                    ClientId = 1,
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetBookingHistory(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Empty(listData);
        }

        [Fact]
        public async Task GetBookingHistory_UnsuitableBooking()
        {
            Setup();

            var firstSlot = new Slot()
            {
                Id = 1,
                CoachId = 3,
                Coach = new Coach()
                {
                    FirstName = "Maga",
                    LastName = "Tigr"
                },
                Date = new DateTime(2020, 6, 16),
                StartTime = new TimeSpan(23, 0, 0)
            };
            var secondSlot = new Slot()
            {
                Id = 2,
                CoachId = 2,
                Date = new DateTime(2021, 4, 20),
                StartTime = new TimeSpan(18, 0, 0)
            };

            var slots = new List<Slot>() { firstSlot, secondSlot };
            var bookings = new List<Booking>()
            {
                new() {
                    Id = 1,
                    SlotId = 2,
                    Slot = firstSlot,
                    ClientId = 2,
                    Service = new Service() {Price = 5555}
                },
                new() { Id = 2, SlotId = 2, Slot = secondSlot, ClientId = 2}
            };

            _applicationContextMock.Setup(x => x.Slots).ReturnsDbSet(slots);
            _applicationContextMock.Setup(x => x.Bookings).ReturnsDbSet(bookings);

            _testedService = new BookingService(Logger, _applicationContextMock.Object);

            var result =
                await _testedService.GetBookingHistory(1, 1, 1, CancellationToken.None);

            var listData = result.Data.ToArray();

            Assert.Empty(listData);
        }
    }
}

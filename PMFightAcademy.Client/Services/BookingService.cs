using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Mappings;
using PMFightAcademy.Dal;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static PMFightAcademy.Client.Mappings.CoachMapping;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly ApplicationContext _context;

#pragma warning disable 1591
        public BookingService(ILogger<BookingService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
#pragma warning restore 1591

        /// <inheritdoc/>
        public async Task<IEnumerable<Service>> GetServicesForBooking(CancellationToken token)
        {
            var result = await _context.Services
                .OrderBy(service => service.Name)
                .ToArrayAsync(token);

            return !result.Any() ? ReturnResult<Service>() : result.AsEnumerable();
        }

        /// <inheritdoc/>
        public async Task<GetDataContract<Service>> GetServicesForBooking(int pageSize, int page, CancellationToken token)
        {
            var services = await _context.Services
                .OrderBy(service => service.Name)
                .ToListAsync(token);

            var servicesCount = (decimal)(services.Count);

            return new GetDataContract<Service>()
            {
                Data = services.Skip((page - 1) * pageSize).Take(pageSize),
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(servicesCount / pageSize)
                }
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CoachDto>> GetCoachesForBooking(int serviceId, CancellationToken token)
        {            
            var qualifications = await _context.Qualifications
                .Include(x => x.Coach)
                .Include(x => x.Service)
                .Where(q => q.ServiceId == serviceId)
                .ToListAsync(token);

            var allQualifications = _context.Qualifications.Include(x => x.Service);

            var coaches = qualifications.Select(q => 
                CoachWithServicesToCoachDto(q.Coach, qualifications
                    .Where(x => x.CoachId == q.CoachId)
                    .Select(x => x.Service.Name)
                    .ToList()
                    .OrderBy(x => x)
                ))
                .OrderBy(coach => coach.LastName).ThenBy(coach => coach.FirstName)
                .ToList() ?? new List<CoachDto>();

            return coaches;
        }

        /// <inheritdoc/>
        public async Task<GetDataContract<CoachDto>> GetCoachesForBooking(int serviceId, int pageSize, int page, CancellationToken token)
        {
            //todo: add logger 

            var qualifications = await _context.Qualifications                
                .Include(x => x.Coach)
                .Where(q => q.ServiceId == serviceId)
                .ToListAsync(token);

            var allQualifications = _context.Qualifications.Include(x => x.Service);

            var coaches = qualifications.Select(q => CoachWithServicesToCoachDto(q.Coach, 
                allQualifications
                    .Where(x => x.CoachId == q.CoachId)
                    .Select(x => x.Service.Name)
                    .OrderBy(x => x)
                    .ToList()                       // is necessary here!
                ))
                .OrderBy(coach => coach.LastName).ThenBy(coach => coach.FirstName);

            var coachesCount = (decimal)(coaches.Count());

            return new GetDataContract<CoachDto>()
            {
                Data = coaches.Skip((page - 1) * pageSize).Take(pageSize),
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(coachesCount / pageSize)
                }
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetDatesForBooking(int serviceId, int coachId, CancellationToken token)
        {
            var qualifications = await _context.Qualifications.ToArrayAsync(token);
            var slots = await _context.Slots.Where(x => x.Expired == false).ToArrayAsync(token);
            var bookings = await _context.Bookings.ToArrayAsync(token);

            //Check if the coach owns the services
            if (!qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
            {
                _logger.LogInformation($"Qualifications with coach id is {coachId} and service id is {serviceId} are not found");
                return ReturnResult<string>();
            }

            //Find our coaches slots which is not already booked
            //and select only date 
            var result = slots
                .Where(x => x.CoachId == coachId)
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .Select(x => x.Date.ToString(Settings.DateFormat))
                .Distinct()
                .OrderBy(x => x)
                .ToArray();

            return result.Any() ? result.AsEnumerable() : ReturnResult<string>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetTimeSlotsForBooking(int serviceId, int coachId, string date, CancellationToken token)
        {
            var qualifications = await _context.Qualifications.ToArrayAsync(token);
            var slots = await _context.Slots.Where(x => x.Expired == false).ToArrayAsync(token);
            var bookings = await _context.Bookings.ToArrayAsync(token);

            if (!qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
            {
                _logger.LogInformation($"Qualifications with coach id is {coachId} and service id is {serviceId} are not found");
                return ReturnResult<string>();
            }

            //Check by slots by coach Id. Than check if slot is available and not booked.
            //And finally check if date in slot equals to your date.
            var result = slots
                .Where(x => x.CoachId == coachId)
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .Where(x => DateTime.ParseExact(date, Settings.DateFormat, null) == x.Date)
                .Select(x => (new DateTime(1, 1, 1) + x.StartTime).ToString(Settings.TimeFormat))
                .Distinct()
                .OrderBy(x => x)
                .ToArray();

            return result.Any() ? result.AsEnumerable() : ReturnResult<string>();
        }

        /// <inheritdoc/>
        public async Task<bool> AddBooking(BookingDto bookingDto, int clientId, CancellationToken token)
        {
            var slots = await _context.Slots.Where(x => x.Expired == false).ToArrayAsync(token);
            var bookings = await _context.Bookings.ToArrayAsync(token);
            var services = await _context.Services.ToArrayAsync(token);

            //Check if slots with our date and start time exists.
            //Than check if slots are available and not booked.
            //Find find slot with our coach
            var yourSlot = slots
                .Where(x => x.Date == DateTime.ParseExact(bookingDto.Date, Settings.DateFormat, null) && x.StartTime == TimeSpan.Parse(bookingDto.Time))
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .FirstOrDefault(x => x.CoachId == bookingDto.CoachId);

            if (yourSlot == null)
            {
                _logger.LogInformation($"Slot which client {clientId} want to book are not found or already booked");
                return false;
            }

            //Find price for our service
            var price = services.FirstOrDefault(x => x.Id == bookingDto.ServiceId);

            if (price == null)
            {
                _logger.LogInformation($"Service with id {bookingDto.ServiceId} are not found");
                return false;
            }

            //Convert bookingDto to Booking for db
            var booking = BookingMapping.BookingMapFromContractToModel(bookingDto, yourSlot.Id, clientId, price.Price);

            //Save new booking
            await _context.Bookings.AddAsync(booking, token);
            await _context.SaveChangesAsync(token);
            return true;
        }

        /// <inheritdoc/>
        public async Task<GetDataContract<HistoryDto>> GetActiveBookings(int pageSize, int page, int clientId, CancellationToken token)
        {
            var now = DateTime.Now;
            var clientBookings = await GetClientBookingsAsync(clientId, token);

            if (!clientBookings.Any())
            {
                _logger.LogInformation($"Client {clientId} did not book any slots");
                return new GetDataContract<HistoryDto>()
                {

                    Data = ReturnResult<HistoryDto>(),
                    Paggination = new Paggination()
                    {
                        Page = page,
                        TotalPages = 0
                    }
                };
            }

            var activeBookings = (from booking in clientBookings
                                  let date = booking.Slot.Date + booking.Slot.StartTime
                                  where date.Subtract(now).TotalMinutes >= 0
                                  orderby date
                                  select new HistoryDto
                                  (
                                      booking.Service.Name,
                                      booking.Slot.Date,
                                      booking.Slot.StartTime,
                                      booking.Slot.Coach.FirstName,
                                      booking.Slot.Coach.LastName,
                                      booking.Service.Price
                                  )).ToList();

            var activeBookingsCount = (decimal)activeBookings.Count;

            return new GetDataContract<HistoryDto>()
            {
                Data = activeBookings.Skip((page - 1) * pageSize).Take(pageSize),
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(activeBookingsCount / pageSize)
                }
            };
        }

        /// <inheritdoc/>
        public async Task<GetDataContract<HistoryDto>> GetBookingHistory(int pageSize, int page, int clientId, CancellationToken token)
        {
            var now = DateTime.Now;
            var clientBookings = await GetClientBookingsAsync(clientId, token);

            if (!clientBookings.Any())
            {
                _logger.LogInformation($"Client {clientId} did not book any slots");
                return new GetDataContract<HistoryDto>()
                {
                    Data = ReturnResult<HistoryDto>(),
                    Paggination = new Paggination()
                    {
                        Page = page,
                        TotalPages = 0
                    }
                };
            }

            var activeBookings = (from booking in clientBookings
                                  let date = booking.Slot.Date + booking.Slot.StartTime
                                  where date.Subtract(now).TotalMinutes < 0
                                  orderby date descending
                                  select new HistoryDto
                                  (
                                      booking.Service.Name,
                                      booking.Slot.Date,
                                      booking.Slot.StartTime,
                                      booking.Slot.Coach.FirstName,
                                      booking.Slot.Coach.LastName,
                                      booking.Service.Price
                                  )).ToList();

            var activeBookingsCount = (decimal)activeBookings.Count;

            return new GetDataContract<HistoryDto>()
            {
                Data = activeBookings.Skip((page - 1) * pageSize).Take(pageSize),
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(activeBookingsCount / pageSize)
                }
            };
        }

        private async Task<List<Booking>> GetClientBookingsAsync(int clientId, CancellationToken token)
        {
            var bookings = _context.Bookings
                .AsNoTracking()
                .Include(booking => booking.Slot)
                .Include(booking => booking.Slot.Coach)
                .Include(booking => booking.Service);

            return await bookings.Where(booking => booking.ClientId == clientId).ToListAsync(token);
        }

        private static IEnumerable<T> ReturnResult<T>()
        {
            return new List<T>().AsEnumerable();
        }
    }
}

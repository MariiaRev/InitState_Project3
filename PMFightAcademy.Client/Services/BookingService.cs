using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Mappings;
using PMFightAcademy.Client.Contract;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly ClientContext _context;

#pragma warning disable 1591
        public BookingService(ClientContext context)
        {
            _context = context;
        }
#pragma warning restore 1591

        /// <inheritdoc/>
        public Task<IEnumerable<Service>> GetServicesForBooking()
        {
            var result = _context.Services.AsEnumerable();
            if (!result.Any())
                return Task.FromResult(result);

            throw new ArgumentException("Service Collection is empty");
        }

        /// <inheritdoc/>
        public Task<IEnumerable<CoachDto>> GetCoachesForBooking(int serviceId)
        {
            if (_context.Services.AsEnumerable().All(x => x.Id != serviceId))
                throw new ArgumentException("Incorrect service id");

            var coachesId = _context.Qualifications.AsEnumerable()
                .Where(x => x.ServiceId == serviceId).Select(x => x.CoachId);

            //todo: add logger 

            var listResult = new List<CoachDto>();
            foreach (var item in coachesId)
            {
                var coach = _context.Coaches.AsEnumerable().FirstOrDefault(x => x.Id == item);

                if (coach == null)
                    throw new ArgumentException($"Incorrect service id");

                var coachDto = CoachMapping.CoachToCoachDto(coach);

                var services = _context.Qualifications.AsEnumerable()
                    .Where(x => x.CoachId == coach.Id)
                    .Select(x => x.Service.Name).ToList();

                coachDto.Services = services;

                listResult.Add(coachDto);
            }

            return Task.FromResult(listResult.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<string>> GetDatesForBooking(int serviceId, int coachId)
        {
            if (!_context.Qualifications.AsEnumerable().Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                throw new ArgumentException("No same qualification");

            var slots = _context.Slots.AsEnumerable().Where(x => x.CoachId == coachId).ToList();

            if (slots.Count == 0)
                throw new ArgumentException("Available Slot Collection is empty");

            var result = slots
                .Where(x => _context.Bookings.AsEnumerable().All(y => y.SlotId != x.Id))
                .Select(x => x.Date.ToString("MM/dd/yyyy")).ToList();

            if (result.Count == 0)
                throw new ArgumentException("Available Slot Collection is already booked");

            return Task.FromResult(result.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<string>> GetTimeSlotsForBooking(int serviceId, int coachId, string date)
        {
            if (!_context.Qualifications.AsEnumerable().Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                throw new ArgumentException("No same qualification");

            var slots = _context.Slots.AsEnumerable().Where(x => x.CoachId == coachId).ToList();

            if (slots.Count == 0)
                throw new ArgumentException("Available Slot Collection is empty");

            var freeSlots = slots
                .Where(x => _context.Bookings.AsEnumerable().All(y => y.SlotId != x.Id)).ToList();

            if (freeSlots.Count == 0)
                throw new ArgumentException("Available Slot Collection is already booked");

            var result = freeSlots
                .Where(x => x.Date.ToString("MM/dd/yyyy") == date)
                .Select(x => x.StartTime.ToString("HH:mm")).ToList();

            if (result.Count == 0)
                throw new ArgumentException("Available Slot Collection on your date is already booked");

            return Task.FromResult(result.AsEnumerable());
        }

        /// <inheritdoc/>
        public async Task AddBooking(BookingDto bookingDto, int clientId)
        {
            if (bookingDto == null)
                throw new ArgumentException("BookingDTO can not be null");

            if (!_context.Slots.AsEnumerable()
                .Any(x => x.Date.ToString("MM/dd/yyyy") == bookingDto.Date 
                          && x.StartTime.ToString("HH:mm") == bookingDto.Time))
                throw new ArgumentException("Available Slot Collection on your time is empty");

            var slots = _context.Slots.AsEnumerable()
                .Where(x => x.Date.ToString("MM/dd/yyyy") == bookingDto.Date 
                            && x.StartTime.ToString("HH:mm") == bookingDto.Time).ToList();

            var freeSlots = slots
                .Where(x => _context.Bookings.AsEnumerable().All(y => y.SlotId != x.Id)).ToList();

            if (freeSlots.Count == 0)
                throw new ArgumentException("Available Slot Collection  on your time is already booked");

            var yourSlot = freeSlots.FirstOrDefault(x => x.CoachId == bookingDto.CoachId);

            if (yourSlot == null)
                throw new ArgumentException("No same slot on your time with your coach");

            var price = _context.Services.AsEnumerable().FirstOrDefault(x => x.Id == bookingDto.ServiceId);

            if (price == null)
                throw new ArgumentException("No same service in Service Collection");

            var booking = BookingMapping.BookingMapFromContractToModel(bookingDto, yourSlot.Id, clientId, price.Price);

           await _context.Bookings.AddAsync(booking);

           await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<GetDataContract<HistoryDto>> GetActiveBookings(int pageSize, int page, int clientId, CancellationToken token)
        {
            var now = DateTime.Now;
            var clientBookings = await GetClientBookingsAsync(clientId, token);

            if (!clientBookings.Any())
            {
                return new GetDataContract<HistoryDto>();
            }

            var activeBookings = from booking in clientBookings
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
                                 );

            var activeBookingsCount = (decimal)activeBookings.Count();

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
                return new GetDataContract<HistoryDto>();
            }

            var activeBookings = from booking in clientBookings
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
                                 );

            var activeBookingsCount = (decimal)activeBookings.Count();

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
    }
}

﻿using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var result = _context.Services?.ToArray();

            if (result == null || !result.Any())
                return ReturnResult<Service>();

            return Task.FromResult(result.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<CoachDto>> GetCoachesForBooking(int serviceId)
        {
            var services = _context.Services?.ToArray();
            var qualifications = _context.Qualifications?.ToArray();
            var coaches = _context.Coaches?.ToArray();

            if (services == null || qualifications == null || coaches == null)
                return ReturnResult<CoachDto>();

            //Check if service is real
            if (services.All(x => x.Id != serviceId))
                return ReturnResult<CoachDto>();

            //Check if qualifications with our service Id exists 
            var coachesId = qualifications
                .Where(x => x.ServiceId == serviceId)
                .Select(x => x.CoachId).ToArray();

            //todo: add logger 

            var listResult = new List<CoachDto>();
            foreach (var item in coachesId)
            {
                //Check if coach is real
                var coach = coaches.FirstOrDefault(x => x.Id == item);

                if (coach == null)
                    return ReturnResult<CoachDto>();

                //Made CoachDto from coach
                var coachDto = CoachMapping.CoachToCoachDto(coach);

                //Save services for our coach 
                coachDto.Services = qualifications
                    .Where(x => x.CoachId == coach.Id)
                    .Select(x => x.Service.Name);

                listResult.Add(coachDto);
            }

            return Task.FromResult(listResult.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<string>> GetDatesForBooking(int serviceId, int coachId)
        {
            var qualifications = _context.Qualifications?.ToArray();
            var slots = _context.Slots?.ToArray();
            var bookings = _context.Bookings?.ToArray();

            if (qualifications == null || slots == null || bookings == null)
                return ReturnResult<string>();

            //Check if the coach owns the services
            if (!qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                return ReturnResult<string>();

            //Find our coaches slots which is not already booked
            //and select only date 
            var result = slots
                .Where(x => x.CoachId == coachId)
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .Select(x => x.Date.ToString("MM/dd/yyyy"))
                .Distinct()
                .ToArray();

            return result.Any() ? Task.FromResult(result.AsEnumerable()) : ReturnResult<string>();
        }

        /// <inheritdoc/>
        public Task<IEnumerable<string>> GetTimeSlotsForBooking(int serviceId, int coachId, string date)
        {
            var qualifications = _context.Qualifications?.ToArray();
            var slots = _context.Slots?.ToArray();
            var bookings = _context.Bookings?.ToArray();

            if (qualifications == null || slots == null || bookings == null)
                return ReturnResult<string>();

            if (!qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                return ReturnResult<string>();

            //Check by slots by coach Id. Than check if slot is available and not booked.
            //And finally check if date in slot equals to your date.
            var result = slots
                .Where(x => x.CoachId == coachId)
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .Where(x => DateTime.Parse(date.Replace("%2F", ".")) == x.Date)
                .Select(x => (new DateTime(1,1,1) + x.StartTime).ToString("HH:mm"))
                .ToArray();

            return result.Any() ? Task.FromResult(result.AsEnumerable()) : ReturnResult<string>();
        }

        /// <inheritdoc/>
        public async Task<bool> AddBooking(BookingDto bookingDto, int clientId)
        {
            var slots = _context.Slots?.ToArray();
            var bookings = _context.Bookings?.ToArray();
            var services = _context.Services?.ToArray();

            if (slots == null || bookings == null || services == null || bookingDto == null)
                return false;

            //Check if slots with our date and start time exists.
            //Than check if slots are available and not booked.
            //Find find slot with our coach
            var yourSlot = slots
                .Where(x => x.Date == DateTime.Parse(bookingDto.Date) && x.StartTime == TimeSpan.Parse(bookingDto.Time))
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .FirstOrDefault(x => x.CoachId == bookingDto.CoachId);

            if (yourSlot == null)
                return false;

            //Find price for our service
            var price = services.FirstOrDefault(x => x.Id == bookingDto.ServiceId);

            if (price == null)
                return false;

            //Convert bookingDto to Booking for db
            var booking = BookingMapping.BookingMapFromContractToModel(bookingDto, yourSlot.Id, clientId, price.Price);

            //Save new booking
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        private static Task<IEnumerable<T>> ReturnResult<T>()
        {
            return Task.FromResult(new List<T>().AsEnumerable());
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

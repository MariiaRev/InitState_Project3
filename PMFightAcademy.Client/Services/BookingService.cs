using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Mappings;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService
    {
        private readonly ClientContext _context;

#pragma warning disable 1591
        public BookingService(ClientContext context)
        {
            _context = context;
        }
#pragma warning restore 1591

        /// <summary>
        /// Get available services for client booking for Booking Controller
        /// </summary>
        public Task<List<Service>> GetServicesForBooking()
        {
            var result = _context.Services.ToList();
            if (result.Count != 0)
                return Task.FromResult(result);

            throw new ArgumentException("Service Collection is empty");
        }

        /// <summary>
        /// Get available coaches which can provide service with id <paramref name="serviceId"/> for Booking Controller  
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<List<CoachDto>> GetCoachesForBooking(int serviceId)
        {
            if (!_context.Services.Any(x => x.Id == serviceId))
                throw new ArgumentException("Incorrect service id");

            var coachesId = _context.Qualifications
                .Where(x => x.ServiceId == serviceId).Select(x => x.CoachId);

            //todo: add logger 

            var listResult = new List<CoachDto>();
            foreach (var item in coachesId)
            {
                var coach = _context.Coaches.FirstOrDefault(x => x.Id == item);

                if (coach == null)
                    throw new ArgumentException($"Incorrect service id");

                var coachDto = CoachMapping.CoachToCoachDto(coach);

                var services = _context.Qualifications
                    .Where(x => x.CoachId == coach.Id)
                    .Select(x => x.Service.Name).ToList();

                coachDto.Services = services;

                listResult.Add(coachDto);
            }

            return Task.FromResult(listResult);
        }

        /// <summary>
        /// Get available dates to provide a service with id <paramref name="serviceId"/> by coach with id <paramref name="coachId"/>
        /// for Booking Controller
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public Task<List<string>> GetDatesForBooking(int serviceId, int coachId)
        {
            if (!_context.Qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                throw new ArgumentException("No same qualification");

            var slots = _context.Slots.Where(x => x.CoachId == coachId).ToList();

            if (slots.Count == 0)
                throw new ArgumentException("Available Slot Collection is empty");

            var result = slots
                .Where(x => _context.Bookings.All(y => y.SlotId != x.Id))
                .Select(x => x.Date.ToString()).ToList();

            if (result.Count == 0)
                throw new ArgumentException("Available Slot Collection is already booked");

            return Task.FromResult(result);
        }
    }
}

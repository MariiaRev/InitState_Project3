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
        public Task<IEnumerable<Service>> GetServicesForBooking()
        {
            var result = _context.Services?.ToArray();

            if (result == null || !result.Any())
                return null;

            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Get available coaches which can provide service with id <paramref name="serviceId"/> for Booking Controller  
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<IEnumerable<CoachDto>> GetCoachesForBooking(int serviceId)
        {
            var services = _context.Services?.ToArray();
            var qualifications = _context.Qualifications?.ToArray();
            var coaches = _context.Coaches?.ToArray();

            if (services == null || qualifications == null || coaches == null)
                return null;

            //Check if service is real
            if (services.All(x => x.Id != serviceId))
                return null;

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
                    return null;

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

        /// <summary>
        /// Get available dates to provide a service with id <paramref name="serviceId"/> by coach with id <paramref name="coachId"/>
        /// for Booking Controller
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetDatesForBooking(int serviceId, int coachId)
        {
            var qualifications = _context.Qualifications?.ToArray();
            var slots = _context.Slots?.ToArray();
            var bookings = _context.Bookings?.ToArray();

            if (qualifications == null || slots == null || bookings == null)
                return null;

            //Check if the coach owns the services
            if (qualifications.Any(x => x.CoachId == coachId && x.ServiceId == serviceId))
                return null;

            //Find our coaches slots which is not already booked
            //and select only date 
            var result = slots
                .Where(x => x.CoachId == coachId)
                .Where(x => bookings.All(y => y.SlotId != x.Id))
                .Select(x => x.Date.ToString("MM/dd/yyyy")).ToArray();

            return result.Any()? Task.FromResult(result.AsEnumerable()) : null;
        }

        /// <summary>
        /// Get available time slots to provide a service with id <paramref name="serviceId"/> for Booking Controller
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="coachId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a booking for Booking Controller
        /// </summary>
        /// <param name="bookingDto"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
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
    }
}

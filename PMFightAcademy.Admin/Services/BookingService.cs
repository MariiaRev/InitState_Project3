using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly AdminContext _dbContext;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public BookingService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take all slots
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookingContract>> TakeAllBooking()
        {
            var bookings = _dbContext.Bookings.Select(BookingMapping.BookingMapFromModelTToContract);
            return bookings;
        }

        /// <summary>
        /// Take Booking for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public async  Task<IEnumerable<BookingContract>> TakeBookingForCoach(int coachId)
        {
            var bookings = _dbContext.Bookings.Where(x => x.Slot.CoachId == coachId).ToArray();
            return bookings.Select(BookingMapping.BookingMapFromModelTToContract);
        }

        /// <summary>
        /// Take booking for client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingContract>> TakeBookingOnClient(int clientId)
        {
            var bookings = _dbContext.Bookings.Where(x => x.ClientId == clientId);
            return bookings.AsEnumerable().Select(BookingMapping.BookingMapFromModelTToContract); ;
        }

        /// <summary>
        /// Update booking
        /// </summary>
        /// <param name="bookingContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBooking(BookingContract bookingContract, CancellationToken cancellationToken)
        {
            var booking = BookingMapping.BookingMapFromContractToModel(bookingContract);
            try
            {
                _dbContext.Update(booking);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Take clients depends from date
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingContract>> TakeBookingForClientOnDate(int clientId, string start, string end)
        {
            if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
                return new List<BookingContract>();
            if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
                return new List<BookingContract>();
            var bookings = _dbContext.Bookings.Select(x=>x).Where(x => x.ClientId == clientId)
                .Where(x => x.Slot.Date >= dateStart).Where(x => x.Slot.Date <= dateEnd);
            return bookings.AsEnumerable().Select(BookingMapping.BookingMapFromModelTToContract); 
        }

        /// <summary>
        /// Take coaches depends from date
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingContract>> TakeBookingForCoachOnDate(int coachId, string start, string end)
        {
            if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
                return new List<BookingContract>();
            if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
                return new List<BookingContract>();

            var bookings = _dbContext.Bookings.Select(x => x).Where(x => x.Slot.CoachId == coachId)
                .Where(x => x.Slot.Date >= dateStart).Where(x => x.Slot.Date <= dateEnd);
            return bookings.AsEnumerable().Select(BookingMapping.BookingMapFromModelTToContract);
        }

        /// <summary>
        /// Remove booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async  Task<bool> RemoveBooking(int id, CancellationToken cancellationToken)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return false;
            }
            try
            {
                _dbContext.Remove(booking);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly ApplicationContext _dbContext;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public BookingService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take all bookings
        /// </summary>
        public async Task<IEnumerable<BookingReturnContract>> TakeAllBooking()
        {
            var bookings = _dbContext.Bookings.Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
            return bookings;
        }

        /// <summary>
        /// Take Booking for coach
        /// </summary>
        /// <param name="coachId"></param>
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingForCoach(int coachId)
        {
            var bookings = _dbContext.Bookings.Where(x => x.Slot.CoachId == coachId).ToArray();
            return bookings.Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
        }

        /// <summary>
        /// Take booking for client
        /// </summary>
        /// <param name="clientId"></param>
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingOnClient(int clientId)
        {
            var bookings = _dbContext.Bookings.Where(x => x.ClientId == clientId);
            return bookings.AsEnumerable().Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x)); ;
        }

        /// <summary>
        /// Update booking
        /// </summary>
        /// <param name="bookingReturnContract"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> UpdateBooking(
            BookingReturnContract bookingReturnContract, 
            CancellationToken cancellationToken)
        {
            var booking = BookingMapping.BookingMapFromContractToModel(bookingReturnContract);
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
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingForClientOnDate(
            int clientId,
            string start, 
            string end)
        {
            if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
                return new List<BookingReturnContract>();
            if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
                return new List<BookingReturnContract>();
            var bookings = _dbContext.Bookings
                .Select(x => x)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.Slot.Date >= dateStart)
                .Where(x => x.Slot.Date <= dateEnd);
            return bookings.AsEnumerable().Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
        }

        /// <summary>
        /// Take coaches depends from date
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingForCoachOnDate(
            int coachId, 
            string start, 
            string end)
        {
            if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
                return new List<BookingReturnContract>();
            if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
                return new List<BookingReturnContract>();

            var bookings = _dbContext.Bookings.Select(x => x).Where(x => x.Slot.CoachId == coachId)
                .Where(x => x.Slot.Date >= dateStart).Where(x => x.Slot.Date <= dateEnd);
            return bookings.AsEnumerable().Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
        }

        /// <summary>
        /// Remove booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> RemoveBooking(int id, CancellationToken cancellationToken)
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

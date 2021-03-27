using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public BookingService(ILogger<BookingService> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take all bookings
        /// </summary>
        public async Task<IEnumerable<BookingReturnContract>> TakeAllBooking(CancellationToken token)
        {
            var result = _dbContext.Bookings
                .Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));

            return result;
        }

        /// <summary>
        /// Take Booking for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// /// <param name="token"></param>
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingForCoach(int coachId, CancellationToken token)
        {
            var bookings = _dbContext.Bookings
                .Where(x => x.Slot.CoachId == coachId)
                .Select(x => BookingMapping.BookingMapFromModelTToContract(x.Slot, x));

            return bookings;
        }

        /// <summary>
        /// Take booking for client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        public async Task<IEnumerable<BookingReturnContract>> TakeBookingOnClient(int clientId, CancellationToken token)
        {
            var bookings = _dbContext.Bookings
                .Where(x => x.ClientId == clientId)
                .Select(x => BookingMapping.BookingMapFromModelTToContract(x.Slot, x));

            return bookings;
        }

        /// <summary>
        /// Update booking
        /// </summary>
        /// <param name="bookingContract"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> UpdateBooking(
            BookingReturnContract bookingContract, 
            CancellationToken cancellationToken)
        {
            var checkNull = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == bookingContract.Id, cancellationToken);
            if (checkNull == null)
            {
                _logger.LogInformation($"Booking with id {bookingContract.Id} are not found");
                return false;
            }

            var booking = BookingMapping.BookingMapFromContractToModel(bookingContract);
            try
            {
                _dbContext.Update(booking);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Booking with id {booking.Id} is not found");
                return false;
            }

            return true;
        }

        ///// <summary>
        ///// Take clients depends from date
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <param name="token"></param>
        //public async Task<IEnumerable<BookingReturnContract>> TakeBookingForClientOnDate(
        //    int clientId,
        //    string start, 
        //    string end,
        //    CancellationToken token)
        //{
        //    var bookings = await _dbContext.Bookings.ToListAsync(token);


        //    if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
        //        return new List<BookingReturnContract>();

        //    if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
        //        return new List<BookingReturnContract>();

        //    var result = bookings
        //        .Where(x => x.ClientId == clientId)
        //        .Where(x => x.Slot.Date >= dateStart)
        //        .Where(x => x.Slot.Date <= dateEnd);

        //    return result.AsEnumerable().Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
        //}
        ///// <summary>
        ///// Take coaches depends from date
        ///// </summary>
        ///// <param name="coachId"></param>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        //public async Task<IEnumerable<BookingReturnContract>> TakeBookingForCoachOnDate(
        //    int coachId, 
        //    string start, 
        //    string end)
        //{
        //    if (!DateTime.TryParseExact(start, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateStart))
        //        return new List<BookingReturnContract>();
        //    if (!DateTime.TryParseExact(end, "MM.dd.yyyy", null, DateTimeStyles.None, out var dateEnd))
        //        return new List<BookingReturnContract>();

        //    var bookings = _dbContext.Bookings.Select(x => x).Where(x => x.Slot.CoachId == coachId)
        //        .Where(x => x.Slot.Date >= dateStart).Where(x => x.Slot.Date <= dateEnd);
        //    return bookings.AsEnumerable().Select(x=>BookingMapping.BookingMapFromModelTToContract(x.Slot,x));
        //}

        /// <summary>
        /// Remove booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> RemoveBooking(int id, CancellationToken cancellationToken)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (booking == null)
            {
                _logger.LogInformation($"Booking with id {id} are not found");
                return false;
            }
            try
            {
                _dbContext.Remove(booking);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Booking with id {booking.Id} is not updated");
                return false;
            }

            return true;
        }
    }
}

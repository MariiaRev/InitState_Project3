using PMFightAcademy.Admin.Contract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Booking service
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Remove Booking
        /// </summary>
        /// <param name="slotContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> RemoveBooking(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Take all bookings
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<BookingContract>> TakeAllBooking();

        /// <summary>
        /// Take booking for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingContract>> TakeBookingForCoach(int coachId);

        /// <summary>
        /// Take booking for client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingContract>> TakeBookingOnClient(int clientId);

        /// <summary>
        /// Update books
        /// </summary>
        /// <param name="slotContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateBooking(BookingContract slotContract, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingContract>> TakeBookingForClientOnDate(int coachId, string start, string end);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingContract>> TakeBookingForCoachOnDate(int clientId, string start, string end);
    }
}
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
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> RemoveBooking(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Take all bookings
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<BookingReturnContract>> TakeAllBooking(CancellationToken token);

        /// <summary>
        /// Take booking for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingReturnContract>> TakeBookingForCoach(int coachId, CancellationToken token);

        /// <summary>
        /// Take booking for client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IEnumerable<BookingReturnContract>> TakeBookingOnClient(int clientId, CancellationToken token);

        /// <summary>
        /// Update books
        /// </summary>
        /// <param name="slotReturnContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateBooking(BookingUpdateContract slotReturnContract, CancellationToken cancellationToken);


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="coachId"></param>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public Task<IEnumerable<BookingReturnContract>> TakeBookingForClientOnDate(int coachId, string start, string end, CancellationToken token);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <param name="start"></param>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //public Task<IEnumerable<BookingReturnContract>> TakeBookingForCoachOnDate(int clientId, string start, string end);
    }
}
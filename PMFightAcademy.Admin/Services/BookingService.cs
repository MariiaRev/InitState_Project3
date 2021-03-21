using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService
    {
        private readonly AdminContext _context;

#pragma warning disable 1591
        public BookingService(AdminContext context)
        {
            _context = context;
        }
#pragma warning restore 1591

        /// <summary>
        /// Return all booked services for BookingController
        /// </summary>
        public Task<GetDataContract<BookingContract>> GetBookedServices(int pageSize, int page,
            CancellationToken cancellationToken)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Incorrect page or page size");

            var bookings = _context.Bookings.ToArray();
            var bookingPerPages = bookings.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            if (bookingPerPages.Length == 0)
                throw new ArgumentException("Booking Collection is empty");

            var pagination = new Paggination()
            {
                Page = page,
                TotalPages = (int)Math.Ceiling((decimal)bookings.Length / pageSize)
            };

            return Task.FromResult(new GetDataContract<BookingContract>()
            {
                Data = bookingPerPages.Select(BookingMapping.CoachMapFromModelTToContract).ToArray(),
                Paggination = pagination
            });
        }


        /// <summary>
        /// select booked services on person for BookingController
        /// </summary>
        /// <returns></returns>
        public Task<List<BookingContract>> GetBookedServiceForClient(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
                throw new ArgumentException("Incorrect id");

            var bookings = _context.Bookings.Where(x => x.ClientId == id).ToList();

            if (bookings.Count == 0)
                throw new ArgumentException("Booking Collection is empty");

            return Task.FromResult(bookings.Select(BookingMapping.CoachMapFromModelTToContract).ToList());
        }
    }
}

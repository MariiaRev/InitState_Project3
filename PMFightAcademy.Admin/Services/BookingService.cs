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
        public Task<GetDataContract<BookingContract>> GetBookedServices(int pageSize, int page)
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
        /// Select booked services on person for BookingController
        /// </summary>
        public Task<List<BookingContract>> GetBookedServiceForClient(int id)
        {
            if (id < 1)
                throw new ArgumentException("Incorrect id");

            var bookings = _context.Bookings.Where(x => x.ClientId == id).ToList();

            if (bookings.Count == 0)
                throw new ArgumentException("Booking Collection is empty");

            return Task.FromResult(bookings.Select(BookingMapping.CoachMapFromModelTToContract).ToList());
        }

        /// <summary>
        /// Select booked services on coach for BookingController
        /// </summary>
        public Task<List<BookingContract>> GetBookedServiceForCoach(int coachId)
        {
            if (coachId < 1)
                throw new ArgumentException("Incorrect id");

            var slots = _context.Slots.Where(x => x.CoachId == coachId).ToList();

            if (slots.Count == 0)
                throw new ArgumentException("Slot Collection is empty");

            //todo: check linq logic
            var bookings = new List<Booking>();
            foreach (var item in _context.Bookings)
            {
                if (slots.Any(x => x.Id == item.SlotId))
                    bookings.Add(item);
            }

            if (bookings.Count == 0)
                throw new ArgumentException("Booking Collection is empty");

            return Task.FromResult(bookings.Select(BookingMapping.CoachMapFromModelTToContract).ToList());
        }

        /// <summary>
        /// Delete a book for BookingController
        /// </summary>
        public async Task DeleteBook(BookingContract bookId, CancellationToken cancellationToken)
        {
            if (bookId == null)
                throw new ArgumentException("Contract can not be null");

            var booking = BookingMapping.BookingMapFromContractToModel(bookId);

            var checkBooking = _context.Bookings.FirstOrDefault(p => p.Id == booking.Id);

            if (checkBooking == null)
                throw new ArgumentException("No same booking in db");

            _context.Remove(checkBooking);

            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Update file for BookingController
        /// </summary>
        public async Task UpdateBook(BookingContract newBook, CancellationToken cancellationToken)
        {
            if (newBook == null)
                throw new ArgumentException("Contract can not be null");

            var booking = BookingMapping.BookingMapFromContractToModel(newBook);

            var checkBooking = _context.Bookings.FirstOrDefault(p => p.Id == booking.Id);

            if (checkBooking == null)
                throw new ArgumentException("No same booking in db");

            _context.Update(checkBooking);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Booking controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("BookingController for check books for admin and remove some")]
    public class BookingController : ControllerBase
    {
        private readonly AdminContext _context;

        /// <summary>
        /// Constructor for booking
        /// </summary>
        public BookingController(AdminContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all booked services
        /// </summary>
        ///  <param name="pageSize">The count of books to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what can be booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Return all booked services 
        /// if notFounded return NF
        /// </remarks>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServices([FromRoute] int pageSize,
            [FromRoute] int page,
            CancellationToken cancellationToken)
        {
            if (page < 1 || pageSize < 1)
                return NotFound("Incorrect page or page size");

            var bookings = _context.Bookings.ToArray();
            var bookingPerPages = bookings.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            if (bookingPerPages.Length == 0)
                return NotFound("Booking Collection is empty");

            var pagination = new Paggination()
            {
                Page = page,
                TotalPages = (int)Math.Ceiling((decimal)bookings.Length / pageSize)
            };

            return Ok(new GetDataContract<BookingContract>()
            {
                Data = bookingPerPages.Select(BookingMapping.CoachMapFromModelTToContract).ToArray(),
                Paggination = pagination
            });
        }

        /// <summary>
        /// select booked services on person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what is booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots
        /// </returns>
        /// <remarks>
        /// Return list about booked info for Client
        /// not founded if no Client 
        /// </remarks>
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForClient(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
                return NotFound("Incorrect id");

            var bookings = _context.Bookings.Where(x => x.ClientId == id).ToList();

            if (bookings.Count == 0)
                return NotFound("Booking Collection is empty");

            return Ok(bookings.Select(BookingMapping.CoachMapFromModelTToContract).ToList());
        }

        /// <summary>
        /// Select booked services on coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots
        /// </returns>
        /// <remarks>
        /// Return list about booked info for coach
        /// not founded if no coaches 
        /// </remarks>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForCoach(int coachId, CancellationToken cancellationToken)
        {
            if (coachId < 1)
                return NotFound("Incorrect id");

            var slots = _context.Slots.Where(x => x.CoachId == coachId).ToList();

            //todo: check linq logic
            var bookings = _context.Bookings
                .Where(x => slots.Any(y => y.Id == x.SlotId)).ToList();

            if (bookings.Count == 0)
                return NotFound("Booking Collection is empty");

            return Ok(bookings.Select(BookingMapping.CoachMapFromModelTToContract).ToList());
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return if book is successful deleted
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Use for delete book , using post method
        /// return ok if successes
        /// return Not found if not founded 
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteBook([FromBody] BookingContract bookId, CancellationToken cancellationToken)
        {
            if (bookId == null)
                return NotFound("Contract can not be null");

            var booking = BookingMapping.BookingMapFromContractToModel(bookId);

            var checkBooking = _context.Bookings.FirstOrDefault(p => p.Id == booking.Id);

            if (checkBooking == null)
                return NotFound("No same booking in db");

            _context.Remove(checkBooking);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Update file
        /// </summary>
        /// <param name="newBook"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return if book is successful updated
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Send with the same Id new values
        /// and they will be updated
        /// </remarks>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateBook(BookingContract newBook, CancellationToken cancellationToken)
        {
            if (newBook == null)
                return NotFound("Contract can not be null");

            var booking = BookingMapping.BookingMapFromContractToModel(newBook);

            var checkBooking = _context.Bookings.FirstOrDefault(p => p.Id == booking.Id);

            if (checkBooking == null)
                return NotFound("No same booking in db");

            _context.Update(checkBooking);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }


        ///// <Not useble part for create book>
        ///// Get book services 
        ///// </summary>
        ///// <param name="train"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return ok if its booked
        ///// <see cref="HttpStatusCode.NotFound"/> return not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return a list of (DTO) if OK
        ///// Not found is some not founded to book
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpPost("book")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> BookService([FromBody]BookingContract train)
        //{
        //    throw  new NotImplementedException();
        //}
        ///// <summary>
        ///// Get book services 
        ///// </summary>
        ///// <param name="train"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return ok if its booked
        ///// <see cref="HttpStatusCode.NotFound"/> return not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return a list of (DTO) if OK
        ///// Not found is some not founded to book
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpPost]
        //[ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> CompactBookService([FromBody] BookingContract train)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IBookingService _bookingService;

        /// <summary>
        /// Constructor for booking
        /// </summary>
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        ///// <summary>
        ///// Return all booked services
        ///// </summary>
        /////  <param name="pageSize">The count of books to return at one time.</param>
        ///// <param name="page">The current page number.</param>
        ///// <param name="cancellationToken"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return list of slots what can be booked
        ///// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        ///// <remarks>
        ///// Return all booked services 
        ///// if notFounded return NF
        ///// </remarks>
        //[HttpGet("{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<BookingContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetBookedServices([FromRoute] int pageSize,
        //    [FromRoute] int page,
        //    CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var result = await _bookingService.GetBookedServices(pageSize, page);

        //        return Ok(result);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //}


        /// <summary>
        /// Return all booked services
        /// </summary>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what can be booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Return all booked services 
        /// if notFounded return NF
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServices()
        {
            var bookings = await _bookingService.TakeAllBooking();
                if (bookings.Any())
                {
                    return Ok(bookings);
                }

                return NotFound("No elements");
        }

        /// <summary>
        /// Select booked services on person
        /// </summary>
        /// <param name="clientId"></param>
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
        [ProducesResponseType(typeof(IEnumerable<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForClient(int clientId, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.TakeBookingOnClient(clientId);
            if (bookings.Any())
            {
                return Ok(bookings);
            }

            return NotFound("No books for this client");
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
        [ProducesResponseType(typeof(IEnumerable<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForCoach(int coachId, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.TakeBookingForCoach(coachId);

            if (bookings.Any())
            {
                return Ok(bookings);
            }

            return NotFound("No books for this coach");
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="bookingId"></param>
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
        public async Task<IActionResult> DeleteBook( int bookingId, CancellationToken cancellationToken)
        {
            var deleted = await _bookingService.RemoveBooking(bookingId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Booking");
        }

        /// <summary>
        /// Update file
        /// </summary>
        /// <param name="newBooking"></param>
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
        public async Task<IActionResult> UpdateBook(BookingContract newBooking, CancellationToken cancellationToken)
        {
            var update = await _bookingService.UpdateBooking(newBooking, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound("No Booking");
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

using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

        

        /// <summary>
        /// Return all booked services
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what can be booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// return list of slots what can be booked
        /// or not founded slots
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookingReturnContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServices(CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.TakeAllBooking(cancellationToken);
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
        [ProducesResponseType(typeof(IEnumerable<BookingReturnContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForClient(
            [Range(1, int.MaxValue)] int clientId, 
            CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.TakeBookingOnClient(clientId, cancellationToken);
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
        [ProducesResponseType(typeof(IEnumerable<BookingReturnContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForCoach(
            [Range(1, int.MaxValue)] int coachId, 
            CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.TakeBookingForCoach(coachId, cancellationToken);

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
        /// <see cref="HttpStatusCode.OK"/>return if book is successful deleted
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots
        /// <remarks>
        /// Use for delete book 
        /// return ok if successes
        /// return Not found if not founded 
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteBook(
            [Range(1, int.MaxValue)] int bookingId, 
            CancellationToken cancellationToken)
        {
            var deleted = await _bookingService.RemoveBooking(bookingId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Booking with such id");
        }

        /// <summary>
        /// Update booking
        /// </summary>
        /// <param name="newBookingReturn"></param>
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
        public async Task<IActionResult> UpdateBook(
            BookingUpdateContract newBookingReturn,
            CancellationToken cancellationToken)
        {
            var update = await _bookingService.UpdateBooking(newBookingReturn, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound("No Booking ");
        }

    }
}

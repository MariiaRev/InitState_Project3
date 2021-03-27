﻿using Microsoft.AspNetCore.Mvc;
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
        //[ProducesResponseType(typeof(GetDataContract<BookingReturnContract>), (int)HttpStatusCode.OK)]
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
            BookingReturnContract newBookingReturn,
            CancellationToken cancellationToken)
        {
            var update = await _bookingService.UpdateBooking(newBookingReturn, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound("No Booking ");
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
        //public async Task<IActionResult> BookService([FromBody]BookingReturnContract train)
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
        //[ProducesResponseType(typeof(List<BookingReturnContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> CompactBookService([FromBody] BookingReturnContract train)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Select booked services for client on  date space 
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <param name="dateEnd"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="dateStart"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return list of slots what is booked
        ///// <see cref="HttpStatusCode.NotFound"/> not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return list about booked info for Client
        ///// not founded if no Client 
        ///// </remarks>
        //[HttpGet("client/{clientId}/{dateStart}/{dateEnd}")]
        //[ProducesResponseType(typeof(IEnumerable<BookingReturnContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetBookedServiceForClientOnDate(
        //    [Range(1, int.MaxValue)] int clientId, 
        //    string dateStart, 
        //    string dateEnd, 
        //    CancellationToken cancellationToken)
        //{
        //    var bookings = 
        //        await _bookingService.TakeBookingForClientOnDate(clientId, dateStart, dateEnd, cancellationToken);

        //    if (bookings.Any())
        //    {
        //        return Ok(bookings);
        //    }

        //    return NotFound("No books for this client");
        //}

        ///// <summary>
        ///// Select booked services for coach on   date space  
        ///// </summary>
        ///// <param name="coachId"></param>
        ///// <param name="dateEnd"></param>
        ///// <param name="cancellationToken"></param>
        ///// <param name="dateStart"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return list of slots what booked
        ///// <see cref="HttpStatusCode.NotFound"/> not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return list about booked info for coach
        ///// In time range
        ///// if not find will return not fount 
        ///// </remarks>
        //[HttpGet("coach/{coachId}/{dateStart}/{dateEnd}")]
        //[ProducesResponseType(typeof(IEnumerable<BookingReturnContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetBookedServiceForCoachOnDate(
        //    [Range(1, int.MaxValue)] int coachId,
        //    string dateStart, 
        //    string dateEnd, 
        //    CancellationToken cancellationToken)
        //{
        //    var bookings = 
        //        await _bookingService.TakeBookingForClientOnDate(coachId, dateStart, dateEnd, cancellationToken);

        //    if (bookings.Any())
        //    {
        //        return Ok(bookings);
        //    }

        //    return NotFound("No books for this coach");
        //}
    }
}

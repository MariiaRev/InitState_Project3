using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services;
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
        private readonly BookingService _bookingService;

        /// <summary>
        /// Constructor for booking
        /// </summary>
        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
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
            try
            {
                var result = await _bookingService.GetBookedServices(pageSize, page);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
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
        [ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForClient(int clientId, CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _bookingService.GetBookedServiceForClient(clientId);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
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
            try
            {
                var result =
                    await _bookingService.GetBookedServiceForCoach(coachId);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
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
            try
            {
                await _bookingService.DeleteBook(bookId, cancellationToken);

                return Ok();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
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
            try
            {
                await _bookingService.UpdateBook(newBook, cancellationToken);

                return Ok();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
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

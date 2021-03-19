using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Service controller for signup for a service/workout.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for working with booking and getting active booking list or booking history.")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        /// <summary>
        /// Gets missing in <paramref name="booking"/> data for a booking depending on set filters in <paramref name="booking"/>.
        /// </summary>
        /// <param name="booking">Booking filters.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if user is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if user is authorized and there is at least one element that corresponds set filters.
        /// <see cref="HttpStatusCode.NotFound"/> if user is authorized and there is no element that corresponds set filters.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if user is unauthorized.
        /// Returns OK if user is authorized and there is at least one element that corresponds set filters.
        /// Returns NotFound if user is authorized and there is no element that corresponds set filters.
        /// </remarks>
        [HttpPost("getMissingData")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<DataForBookingDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public Task<IActionResult> GetMissingDataForBooking([FromBody] BookingDto booking)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a booking.
        /// </summary>
        /// <param name="booking">Booking filters.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if user is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if user is authorized and a booking was successfully added.
        /// <see cref="HttpStatusCode.BadRequest"/> if user is authorized 
        /// and there is at least one missing filter or booking time is not available anymore.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if user is unauthorized.
        /// Returns OK if user is authorized and a booking was successfully added.
        /// Returns BadRequest if user is authorized and there is at least one missing filter
        /// and there is at least one missing filter or booking time is not available anymore.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> AddBooking([FromBody] BookingDto booking)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets active bookings of services.
        /// </summary>
        /// <param name="pageSize">The count of active booking records to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if user is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if user is authorized and there is at least one record in the active booking list.
        /// <see cref="HttpStatusCode.NotFound"/> if user is authorized and there is no record in the active booking list.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if user is unauthorized.
        /// Returns OK if user is authorized and there is at least one record in the active booking list.
        /// Returns NotFound if user is authorized and there is no record in the active booking list.
        /// </remarks>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDataContract<HistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public Task<IActionResult> GetActiveBookings([FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets booking history.
        /// </summary>
        /// <param name="pageSize">The count of history records to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if user is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if user is authorized and there is at least one record in the history.
        /// <see cref="HttpStatusCode.NotFound"/> if user is authorized and there is no record in the history.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if user is unauthorized.
        /// Returns OK if user is authorized and there is at least one record in the history.
        /// Returns NotFound if user is authorized and there is no record in the history.
        /// </remarks>
        [HttpGet("history/{pageSize}/{page}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDataContract<HistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public Task<IActionResult> GetHistory([FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
        }
    }
}

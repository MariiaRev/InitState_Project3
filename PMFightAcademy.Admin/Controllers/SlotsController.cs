using System;
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
using PMFightAcademy.Dal.Models;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Slots controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller for creations slots for coaches ")]
    public class SlotsController : ControllerBase
    {
        private readonly ISlotService _slotService;

        /// <summary>
        /// Slots controller
        /// </summary>
        public SlotsController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        /// <summary>
        /// Show all slots
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Return list of  slots
        /// return if NotFound
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllSlots(CancellationToken cancellationToken)
        {
            
            var slots = await _slotService.TakeAllSlots();

            if (slots.Any())
            {
                return Ok(slots);
            }

            return NotFound();

        }

        /// <summary>
        /// Show slots for coach
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Return list of  slots for coach
        /// return if NotFound
        /// </remarks>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForCoach([FromRoute, Range(1, int.MaxValue)] int coachId)
        {

            var slots = await _slotService.TakeSlotsForCoach(coachId);

            if (slots.Any())
            {
                return Ok(slots);
            }

            return NotFound("No coaches with that id ");
        }

        /// <summary>
        /// Show slots on date
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Return list of  slots for chosen date
        /// </remarks>
        [HttpGet("date/{date}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForDates([FromRoute] string date)
        {
            var slots = await _slotService.TakeAllOnDate(date);

            if (slots.Any())
            {
                return Ok(slots);
            }
            return NotFound();
        }

        /// <summary>
        /// Create Slots
        /// </summary>
        /// <param name="createSlots"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.Conflict"/> if service not founded</returns>
        /// <remarks>
        /// Use for create slots , return ok if added
        /// and conflict if already added
        /// </remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateSlots(
            IEnumerable<SlotsReturnContract> createSlots,
            CancellationToken cancellationToken)
        {
            var added =   await _slotService.AddListOfSlots(createSlots, cancellationToken);
            if (!added)
            {
                return Conflict();
            }
            return Ok();
        }

        /// <summary>
        /// update slots
        /// </summary>
        /// <param name="createSlots"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Use for update slots , send a slot with new fields
        /// </remarks>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSlot(
            [FromBody] SlotsReturnContract createSlots,
            CancellationToken cancellationToken)
        {
            var update = await _slotService.UpdateSlot(createSlots, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound("No slot");
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded
        /// </returns>
        /// <remarks>
        /// Use for delete slots
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSlots(
            IEnumerable<int> slotId, 
            CancellationToken cancellationToken)
        {
            var deleted = await _slotService.RemoveSlotRange(slotId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Service");
        }

    }
}

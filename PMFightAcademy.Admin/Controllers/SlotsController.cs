using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;

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
        private readonly IWorkWithIdService _checkId;

        /// <summary>
        /// Slots controller
        /// </summary>
        public SlotsController(ISlotService slotService , IWorkWithIdService checkId)
        {
            _slotService = slotService;
            _checkId = checkId;
        }

        //#region Maded Pagination but not used by JS (TILT)




        ///// <summary>
        ///// Show all slots
        ///// </summary>
        ///// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        ///// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        ///// <remarks>
        ///// Return list of  slots
        ///// return if NotFound
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>),(int) HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetAllSlots([FromRoute] int pageSize, [FromRoute] int page, CancellationToken cancellationToken)
        //{
        //    GetDataContract<SlotsCreateContract> slots;
        //    try
        //    {
        //        slots = await _slotService.TakeAllSlots(pageSize, page);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return NotFound(e.Message);
        //    }
            
        //    return Ok(slots);
        //}

        ///// <summary>
        ///// Show slots for coach
        ///// </summary>
        ///// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        ///// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        ///// <remarks>
        ///// Return list of  slots for coach
        ///// return if NotFound
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("coach/{coachId}/{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetSlotsForCoach([FromRoute] int coachId, [FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    GetDataContract<SlotsCreateContract> slots;
        //    try
        //    {
        //        slots = await _slotService.TakeSlotsForCoach(coachId,pageSize, page);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return NotFound(e.Message);
        //    }

        //    return Ok(slots);
        //}

        ///// <summary>
        ///// Show slots on date
        ///// </summary>
        ///// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        ///// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        ///// <remarks>
        ///// Return list of  slots for chosen date
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("date/{date}/{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetSlotsForDates([FromRoute] string date, [FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    GetDataContract<SlotsCreateContract> slots;
        //    var dateToSend = DateTime.Parse(date);
        //    try
        //    {
        //        slots = await _slotService.TakeAllOnDate(dateToSend, pageSize, page);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return NotFound(e.Message);
        //    }

        //    return Ok(slots);
        //}

        //#endregion


        /// <summary>
        /// Show all slots
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Return list of  slots
        /// return if NotFound
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
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
         /// <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// <remarks>
        /// Return list of  slots for coach
        /// return if NotFound
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSlotsForCoach([FromRoute,Range(1, int.MaxValue)] int coachId)
        {
            if(!_checkId.IsCorrectId(coachId))
            {
                return BadRequest("incorrect Id");
            }

            var  slots = await _slotService.TakeSlotsForCoach(coachId);

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
        ///  <see cref="HttpStatusCode.BadRequest"/> if date is incorrect
        /// <remarks>
        /// Return list of  slots for chosen date
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("date/{date}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]

        public async Task<IActionResult> CreateSlots([FromBody] SlotsCreateContract createSlots, CancellationToken cancellationToken)
        {
            try
            {
                await _slotService.AddSlot(createSlots, cancellationToken);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
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
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]

        public async Task<IActionResult> UpdateSlot([FromBody] SlotsCreateContract createSlots, CancellationToken cancellationToken)
        {
            try
            {
                await _slotService.UpdateSlot(createSlots, cancellationToken);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded
        ///  <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// </returns>
        /// <remarks>
        /// Use for delete slots
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteSlots([Range(1, int.MaxValue)] int slotId,CancellationToken cancellationToken)
        {

            if (!_checkId.IsCorrectId(slotId))
            {
                return BadRequest("incorrect Id");
            }

            var deleted = await _slotService.RemoveSlot(slotId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Service");
        }


        /// <summary>
        /// Show slots for coach
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded
        ///  <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// </returns>
        /// <remarks>
        /// Return list of  slots for coach in time range
        /// return not fount if its is empty
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("coach/{coachId}/{dateStart}/{dateEnd}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSlotsForCoachFromDateToDate([Range(1, int.MaxValue)] int coachId,string dateStart,string dateEnd)
        {
            if (!_checkId.IsCorrectId(coachId))
            {
                return BadRequest("incorrect Id");
            }

            var slots = await _slotService.TakeSlotsForCoachOnDates(coachId,dateStart,dateEnd);

            if (slots.Any())
            {
                return Ok(slots);
            }

            return NotFound("No slots for this coaches on this dates ");
        }
        

    }
}

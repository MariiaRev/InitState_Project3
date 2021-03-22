﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Services;
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
        private readonly SlotService _slotService;

        /// <summary>
        /// Slots controller
        /// </summary>
        public SlotsController(SlotService slotService)
        {
            _slotService = slotService;
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
            IEnumerable<SlotsCreateContract> slots;
            try
            {
                slots = await _slotService.TakeAllSlots();
               
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(slots);
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
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForCoach([FromRoute] int coachId)
        {
            IEnumerable<SlotsCreateContract> slots;
            try
            {
                slots = await _slotService.TakeSlotsForCoach(coachId);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(slots);
        }

        /// <summary>
        /// Show slots on date
        /// </summary>
        /// <returns><see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Return list of  slots for chosen date
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("date/{date}")]
        [ProducesResponseType(typeof(IEnumerable<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForDates([FromRoute] string date)
        {
            IEnumerable<SlotsCreateContract> slots;
            var dateToSend = DateTime.Parse(date);
            try
            {
                slots = await _slotService.TakeAllOnDate(dateToSend);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(slots);
        }



        /// <summary>
        /// Create Slots
        /// </summary>
        /// <param name="createSlots"></param>
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
        public async Task<IActionResult> CreateSlots([FromBody] SlotsCreateContract createSlots)
        {
            try
            {
                await _slotService.AddSlot(createSlots);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Create Slots
        /// </summary>
        /// <param name="createSlots"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded</returns>
        /// <remarks>
        /// Use for delete slots
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSlots([FromBody] SlotsCreateContract createSlots)
        {
            try
            {
                await _slotService.RemoveSlot(createSlots);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
    }
}

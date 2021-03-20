using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
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
        /// <summary>
        /// Slots controller
        /// </summary>
        public SlotsController()
        {

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
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>),(int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllSlots([FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
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
        [HttpGet("{coachId}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForCoach([FromRoute] int coachId, [FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
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
        [HttpGet("{date}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<SlotsCreateContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSlotsForDates([FromRoute] string date, [FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}

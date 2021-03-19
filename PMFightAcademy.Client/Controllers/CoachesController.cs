using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using Microsoft.AspNetCore.Authorization;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Coach controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for getting data about coaches.")]
    [Authorize]
    public class CoachesController: ControllerBase
    {
        /// <summary>
        /// Portioned return of coaches data.
        /// </summary>
        /// <param name="pageSize">The count of coaches to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="filter">Optional <c>string</c> filter parameter.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if client is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if client is authorized and there is at least one coach for the corresponding request.
        /// <see cref="HttpStatusCode.NotFound"/> if client is authorized and there is no coach for the corresponding request.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if client is unauthorized.
        /// Returns OK if client is authorized and there is at least one coach for the corresponding request.
        /// Returns NotFound if client is authorized and there is no coach for the corresponding request.
        /// </remarks>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDataContract<CoachDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute]int page, [FromQuery] string filter)
        {
            throw new NotImplementedException();
        }
    }
}

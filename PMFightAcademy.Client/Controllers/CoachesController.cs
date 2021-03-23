using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly ICoachesService _coachesService;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        public CoachesController(ICoachesService coachesService)
        {
            _coachesService = coachesService;
        }

        /// <summary>
        /// Portioned return of coaches data.
        /// </summary>
        /// <param name="pageSize">The count of coaches to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="filter">Optional <c>string</c> filter parameter - searching by coach's first or last name.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if client is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> with coaches list if client is authorized and there is at least one coach for the corresponding request.
        /// <see cref="HttpStatusCode.NotFound"/> with <c>string</c> message if client is authorized and there is no coach for the corresponding request.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if client is unauthorized.
        /// Returns OK with coaches list if client is authorized and there is at least one coach for the corresponding request.
        /// Returns NotFound with <c>string</c> message if client is authorized and there is no coach for the corresponding request.
        /// </remarks>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDataContract<CoachDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(
            [FromRoute, Range(1, int.MaxValue)] int pageSize,
            [FromRoute, Range(1, int.MaxValue)] int page,
            [FromQuery] string filter,
#pragma warning disable CS1573
            CancellationToken token)            // no need of token in matching param tag in the XML comment
#pragma warning restore CS1573 
        {
            var coaches = await _coachesService.GetCoaches(pageSize, page, token, filter);

            if (!coaches.Data.Any())
            {
                string message = $"There is no coach on page {page}";

                if (filter != null)
                {
                    message += $" matched the filter '{filter}'";
                }

                return NotFound($"{message}.");
            }

            return Ok(coaches);
        }
    }
}

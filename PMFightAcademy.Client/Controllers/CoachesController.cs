using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
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
        /// <param name="token"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.Unauthorized"/> if client is unauthorized.
        /// <see cref="HttpStatusCode.OK"/> if client is authorized 
        /// with coaches list if there is at least one coach for the corresponding request 
        /// and with empty list if there is no coach for the corresponding request.
        /// </returns>
        /// <remarks>
        /// Returns Unauthorized if client is unauthorized.
        /// Returns OK if client is authorized 
        /// with coaches list if there is at least one coach for the corresponding request 
        /// and with empty list if there is no coach for the corresponding request.
        /// </remarks>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GetDataContract<CoachDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(
            [FromRoute, Range(1, int.MaxValue)] int pageSize,
            [FromRoute, Range(1, int.MaxValue)] int page,
            [FromQuery] string filter,
            CancellationToken token)
        {
            var coaches = await _coachesService.GetCoaches(pageSize, page, token, filter);

            return Ok(coaches);
        }
    }
}

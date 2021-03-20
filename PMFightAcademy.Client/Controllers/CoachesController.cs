using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using Microsoft.AspNetCore.Authorization;
using PMFightAcademy.Client.DataBase;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
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
        private readonly ClientContext _dbContext;


        public CoachesController(ClientContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Portioned return of coaches data.
        /// </summary>
        /// <param name="pageSize">The count of coaches to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="filter">Optional <c>string</c> filter parameter.</param>
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
        public async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute]int page, [FromQuery] string filter)
        {
            throw new NotImplementedException();
        }


        //Test method for take one coach
        //[HttpGet("{coachId}")]
        //public async Task<IActionResult> GetCoach(int coachId)
        //{
        //   var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachId);
        //   return Ok(coach);
        //}
    }
}

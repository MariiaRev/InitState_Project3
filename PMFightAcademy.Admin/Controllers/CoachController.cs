﻿using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Coach controller 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller for work with coach ")]
    public class CoachController : ControllerBase
    {

        private readonly ICoachService _coachService;

        /// <summary>
        /// Constructor for controller
        /// </summary>
        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }



        /// <summary>
        /// Get list of Coaches
        /// </summary>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> Get list of coaches
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        /// </returns>
        /// <remarks>
        /// Use for get all coach , if successes must return a list of coaches
        /// if not,  return Not Found
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CoachContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]

        public async Task<IActionResult> GetAllCoaches()
        {
            var coaches = await _coachService.TakeAllCoaches();

            if (coaches.Any())
            {
                return Ok(coaches);
            }
            return NotFound("No elements");

        }

        /// <summary>
        /// Return chosen coach
        /// </summary>
        /// <para>
        ///<param name="coachId"></param>
        /// </para>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        /// </returns>
        /// <remarks>
        /// Use for get one coach , if successes must return a coach
        /// if not return Not Found
        /// </remarks>
        [HttpGet("{coachId}")]
        [ProducesResponseType(typeof(CoachContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoach([Range(1, int.MaxValue)] int coachId,CancellationToken cancellationToken)
        {

            var coach = await _coachService.TakeCoach(coachId, cancellationToken);

            if (coach != null)
            {
                return Ok(coach);
            }

            return NotFound("No element");

        }


        /// <summary>
        /// Create coach
        /// </summary>
        /// <param name="coach"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add a coach to coaches
        /// <see cref="HttpStatusCode.Conflict"/> if this coach is already registered
        /// </returns>
        /// <remarks>
        /// Use for create coach, send a coach
        /// if it will be added return ok else  return conflict
        /// if it is already added
        /// if not,  return Not Found
        /// </remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateCoach([FromBody] CoachContract coach, CancellationToken cancellationToken)
        {
            var coachAdd =  await _coachService.AddCoach(coach, cancellationToken);
            
            if (coachAdd)
            {
                return Ok();
            }

            return Conflict();
        }

        /// <summary>
        /// update coach
        /// </summary>
        /// <param name="coach"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add a coach to coaches
        /// <see cref="HttpStatusCode.NotFound"/> if this coach is already registered
        /// </returns>
        /// <remarks>
        /// Use for update info about coach
        /// Send coach with id , and new fields what need to be updated
        /// if not, return Not Found
        /// </remarks>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCoach([FromBody] CoachContract coach, CancellationToken cancellationToken)
        {
            var update = await _coachService.UpdateCoach(coach, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Delete coach
        /// </summary>
        /// <para>
        ///<param name="coachId"></param>
        /// <param name = "cancellationToken"></param>
        /// </para>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        /// </returns>
        /// <remarks>
        /// Use for delete coach
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCoach([Range(1, int.MaxValue)] int coachId, CancellationToken cancellationToken)
        {
            var deleted = await _coachService.DeleteCoach(coachId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No coach");

        }
    }
}

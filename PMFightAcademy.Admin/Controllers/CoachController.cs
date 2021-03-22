using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using Swashbuckle.AspNetCore.Annotations;

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
        
        private readonly CoachService _coachService;

        /// <summary>
       /// Constructor for controller
       /// </summary>
       public CoachController(CoachService coachService)
        {
            _coachService = coachService;
        }


        ///// <Pages return coaches>
        ///// Get list of Coaches
        ///// </summary>
        ///// <returns>
        ///// <param name="pageSize">The count of coaches to return at one time.</param>
        ///// <param name="page">The current page number.</param>
        ///// <see cref="HttpStatusCode.OK"/> Get list of coaches
        ///// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        ///// </returns>
        ///// <remarks>
        ///// Use for get all coach , if successes must return a list of coaches
        ///// if not,  return Not Found
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<CoachContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetCoaches([FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    throw new NotImplementedException();
        //}

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
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CoachContract>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllCoaches()
        {
            IEnumerable<CoachContract> coaches;
            try
            {
                coaches = await _coachService.TakeAllCoaches();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(coaches.ToList());

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
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{coachId}")]
        [ProducesResponseType(typeof(CoachContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoach (int coachId)
        {
            
            CoachContract coach;
           try
           {
                coach = await _coachService.TakeCoach(coachId);
           }
           catch (ArgumentException e)
           {
               return NotFound(e.Message);
           }

           return Ok(coach);
        }


        /// <summary>
        /// Create coach
        /// </summary>
        /// <param name="coach"></param>
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
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateCoach([FromBody] CoachContract coach)
        {
            try
            {
                await _coachService.AddCoach(coach);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// update coach
        /// </summary>
        /// <param name="coach"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add a coach to coaches
        /// <see cref="HttpStatusCode.NotFound"/> if this coach is already registered 
        /// </returns>
        /// <remarks>
        /// Use for update info about coach
        /// Send coach with id , and new fields what need to be updated
        /// if not, return Not Found
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCoach([FromBody] CoachContract coach)
        {
            try
            {
                await _coachService.UpdateCoach(coach);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Delete coach
        /// </summary>
        /// <para>
        ///<param coachId="coachId"></param>
        /// </para>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        /// </returns>
        /// <remarks>
        /// Use for delete coach
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [ProducesResponseType(typeof(CoachContract), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCoach(CoachContract coachId)
        {
            try
            {
                await _coachService.DeleteCoach(coachId);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
            }

            return Ok();
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
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
        private readonly AdminContext _dbContext;

        /// <summary>
       /// Constructor for controller
       /// </summary>
       public CoachController(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get list of Coaches
        /// </summary>
        /// <returns>
        /// <param name="pageSize">The count of coaches to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <see cref="HttpStatusCode.OK"/> Get list of coaches
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches yet is empty
        /// </returns>
        /// <remarks>
        /// Use for get all coach , if successes must return a list of coaches
        /// if not,  return Not Found
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<CoachContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoaches([FromRoute] int pageSize, [FromRoute] int page)
       {
           throw new NotImplementedException();
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
           throw new NotImplementedException();
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
            // for test , not implemented method
            try
            {
                Coach newCoach = new Coach()
                {
                    FirstName = coach.FirstName,
                    LastName = coach.LastName,
                };
                await _dbContext.AddAsync(newCoach);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return Conflict();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


    }
}

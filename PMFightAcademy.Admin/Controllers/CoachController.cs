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
    /// Coach controller 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller for work with coach ")]
    public class CoachController : ControllerBase
    {
       /// <summary>
       /// Constructor for controller
       /// </summary>
       public CoachController()
        {

        }

       ///// <summary>
        ///// Function for add vacation to coach
        ///// </summary>
        ///// <param name="dateStart"></param>
        ///// <param name="dataEnd"></param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("vacation/{dateStart}")]
        //public async Task<IActionResult> CreateCoach(string dateStart, string dataEnd)
        //{
        //    throw new NotImplementedException();
        //}

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
        ///<param coachId="coachId"></param>
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
        /// Add qualification
        /// </summary>
        /// <param name="qualification"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.Conflict"/> if qualification is already added
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        /// <remarks>
        /// You can use this 2 times
        /// in coach screen and service screen
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpPost("addService")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddQualification(QualificationContract qualification)
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
        public async Task<IActionResult> CreateCoach([FromBody] Coach coach)
        {
            throw new NotImplementedException();
        }


    }
}

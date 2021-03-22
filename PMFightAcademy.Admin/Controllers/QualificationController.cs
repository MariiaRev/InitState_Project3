using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Qualification controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller For create Qualification and control them")]
    public class QualificationController : ControllerBase
    {
        private readonly QualificationService _qualificationService;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="qualificationService"></param>
        public QualificationController(QualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }
        /// <summary>
        /// get list  qualifications  for Coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        /// <remarks>
        /// Return qualifications for coach
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForCoach([FromRoute] int coachId)
        {
            IEnumerable<Service> services;
            try
            {
                services = await _qualificationService.GetServicesForCoach(coachId);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(services);
        }
        /// <summary>
        /// get list of  qualifications for Service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        /// <remarks>
        /// Return qualifications for services
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpGet("service/{serviceId}")]
        [ProducesResponseType(typeof(IEnumerable<CoachContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForService([FromRoute] int serviceId)
        {
            IEnumerable<CoachContract> coaches;
            try
            {
                coaches = await _qualificationService.GetCoachesForService(serviceId);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(coaches);
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
        /// Use for add Qualification
        /// Usable 2 time is Coach and Service screen
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddQualification(QualificationContract qualification)
        {
            try
            {
               await _qualificationService.AddQualification(qualification);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="qualification"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        /// <remarks>
        /// Use for delete Qualification
        /// Usable 2 time is Coach and Service screen
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteQualification(QualificationContract qualification)
        {
            try
            {
                await _qualificationService.DeleteQualification(qualification);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
        #region JS 
        ///// <summary>
        ///// get list  qualifications  for Coach
        ///// </summary>
        ///// <param name="coachId"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/> return a coach with such name
        ///// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        ///// <remarks>
        ///// Return qualifications for coach
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception> 
        //[HttpGet("coach/{coachId}/{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<QualificationContract>),(int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetQualificationForCoach([FromRoute] int coachId, [FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// get list of  qualifications for Service
        ///// </summary>
        ///// <param name="serviceId"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/> return a coach with such name
        ///// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        ///// <remarks>
        ///// Return qualifications for services
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception> 
        //[HttpGet("service/{serviceId}/{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<QualificationContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetQualificationForService([FromRoute] int serviceId, [FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}

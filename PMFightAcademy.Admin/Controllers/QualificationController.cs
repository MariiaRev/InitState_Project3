using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

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
        private readonly IQualificationService _qualificationService;
        

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="qualificationService"></param>
        /// <param name="checkId"></param>
        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }
        /// <summary>
        /// get list  qualifications  for Coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// </returns>
        /// <remarks>
        /// Return qualifications for coach
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetQualificationForCoach([FromRoute ,Range(1, int.MaxValue)] int coachId)
        {

            var services = await _qualificationService.GetServicesForCoach(coachId);
            if (services.Any())
            {
                return Ok(services);
            }

            return NotFound("No elements");
        }
        /// <summary>
        /// get list of  qualifications for Service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// </returns>
        /// <remarks>
        /// Return qualifications for services
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpGet("service/{serviceId}")]
        [ProducesResponseType(typeof(IEnumerable<CoachContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetQualificationForService([FromRoute ,Range(1, int.MaxValue)] int serviceId)
        {

            var coaches = await _qualificationService.GetCoachesForService(serviceId);
            if (coaches.Any())
            {
                return Ok(coaches);
            }

            return NotFound("No elements");

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
        public async Task<IActionResult> AddQualification(QualificationContract qualification, CancellationToken cancellationToken)
        {
            try
            {
               await _qualificationService.AddQualification(qualification, cancellationToken);
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
        /// <param name="qualificationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// <see cref="HttpStatusCode.BadRequest"/> if id is incorrect
        /// </returns>
        /// <remarks>
        /// Use for delete Qualification
        /// Usable 2 time is Coach and Service screen
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception> 
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteQualification([Range(1, int.MaxValue)] int qualificationId, CancellationToken cancellationToken)
        {

            var deleted = await _qualificationService.DeleteQualification(qualificationId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Qualification");
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

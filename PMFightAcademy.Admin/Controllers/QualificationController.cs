using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.Models;
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
    /// Qualification controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller For create Qualification and control them")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="qualificationService"></param>
        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }

        /// <summary>
        /// get list  qualifications  for Coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// </returns>
        /// <remarks>
        /// Return qualifications for coach
        /// </remarks>
        [HttpGet("coach/qualification/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<QualificationContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForCoach(
            [FromRoute, Range(1, int.MaxValue)] int coachId,
            CancellationToken cancellationToken)
        {

            var services =
                await _qualificationService.GetQualificationsForCoach(coachId, cancellationToken);
            if (services.Any())
            {
                return Ok(services);
            }

            return NotFound("No elements");
        }

        /// <summary>
        /// get list  qualifications  for Service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// </returns>
        /// <remarks>
        /// Return qualifications for Service
        /// </remarks>
        [HttpGet("service/qualification/{serviceId}")]
        [ProducesResponseType(typeof(IEnumerable<QualificationContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForService(
            [FromRoute, Range(1, int.MaxValue)] int serviceId,
            CancellationToken cancellationToken)
        {
            var services =
                await _qualificationService.GetQualificationsForService(serviceId, cancellationToken);
            if (services.Any())
            {
                return Ok(services);
            }

            return NotFound("No elements");
        }

        /// <summary>
        /// get list  Service  for Coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// </returns>
        /// <remarks>
        /// Return qualifications for coach
        /// </remarks>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetServiceForCoach(
            [FromRoute, Range(1, int.MaxValue)] int coachId,
            CancellationToken cancellationToken)
        {

            var services = 
                await _qualificationService.GetServicesForCoach(coachId, cancellationToken);
            if (services.Any())
            {
                return Ok(services);
            }

            return NotFound("No elements");
        }

        /// <summary>
        /// get list of  coach for Service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// </returns>
        /// <remarks>
        /// Return coach for services
        /// </remarks>
        [HttpGet("service/{serviceId}")]
        [ProducesResponseType(typeof(IEnumerable<CoachContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoachForService(
            [FromRoute, Range(1, int.MaxValue)] int serviceId,
            CancellationToken cancellationToken)
        {

            var coaches = 
                await _qualificationService.GetCoachesForService(serviceId, cancellationToken);
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
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet  </returns>
        /// <remarks>
        /// Use for add Qualification
        /// Usable 2 time is Coach and Service screen
        /// </remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddQualification(QualificationContract qualification, CancellationToken cancellationToken)
        {
            var result = await _qualificationService.AddQualification(qualification, cancellationToken);

            if (result)
                return Ok();

            return NotFound();
        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="qualificationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return a coach with such name
        /// <see cref="HttpStatusCode.NotFound"/> if no coaches or service  is empty yet
        /// </returns>
        /// <remarks>
        /// Use for delete Qualification
        /// Usable 2 time is Coach and Service screen
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
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

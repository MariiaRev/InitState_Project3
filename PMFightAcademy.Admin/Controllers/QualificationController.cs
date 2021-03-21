using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Controller For create Qualification and control them")]
    public class QualificationController : ControllerBase
    {

        public QualificationController()
        {

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        [HttpGet("coach/{coachId}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<QualificationContract>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForCoach([FromRoute] int coachId, [FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
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
        [HttpGet("service/{serviceId}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<QualificationContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetQualificationForService([FromRoute] int serviceId, [FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
        }
    }
}

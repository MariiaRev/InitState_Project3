using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Service Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Services controllers for add services")]
    public class ServicesController : ControllerBase
    {
        /// <summary>
        /// Service Controller
        /// </summary>
        public ServicesController()
        {

        }

        /// <summary>
        /// Get all services 
        /// </summary>
        /// <param name="pageSize">The count of services to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add a coach to coaches
        /// <see cref="HttpStatusCode.NotFound"/> return lit of services</returns>
        /// <remarks> Use to Get all service, return services if  all is fine
        /// NotFound if its is already registered
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(GetDataContract<Service>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllServices([FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get needed service 
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/> if service not founded
        /// </returns>
        /// <remarks> Use to Get Service, return service if  all is fine
        /// NotFound if its is already registered
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{serviceId}")]
        [ProducesResponseType(typeof(Service), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetService(int serviceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add services 
        /// </summary>
        /// <param name="service"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.Conflict"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to create Service, return ok if all is fine
        /// Conflict if its is already registered</remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateService([FromBody]Service service)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add services 
        /// </summary>
        /// <param name="service"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.NotFound"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to update service, send service with the same id
        /// and new fields, and it will be update
        /// Conflict if its is already registered</remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateService([FromBody] Service service)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create list of services
        /// </summary>
        /// <param name="listServices"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add list of services
        /// <see cref="HttpStatusCode.NotFound"/> if something not founded
        /// <see cref="HttpStatusCode.Conflict"/> if services is added</returns>
        /// <remarks>
        /// Use to create ServiceList, return ok if all is fine
        /// Conflict if its is already registered
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateServiceList([FromBody] List<Service> listServices)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add services 
        /// </summary>
        /// <param name="service"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.NotFound"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to delete service
        /// Return ok if deleted and not found if BD have not this service
        /// Conflict if its is already registered</remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteService([FromBody] Service service)
        {
            throw new NotImplementedException();
        }

    }
}

﻿using Microsoft.AspNetCore.Mvc;
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
    /// Service Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Services controllers for add services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        /// <summary>
        /// Service Controller
        /// </summary>
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Get all services 
        /// </summary>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add a coach to coaches
        /// <see cref="HttpStatusCode.NotFound"/> return lit of services</returns>
        /// <remarks> Use to Get all service, return services if  all is fine
        /// NotFound if its is service not  registered
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceService.TakeAllServices();
            if (services.Any())
            {
                return Ok(services);
            }
            return NotFound("No services");
        }

        /// <summary>
        /// Get needed service 
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> return service needed 
        /// <see cref="HttpStatusCode.NotFound"/>if service not founded
        /// </returns>
        /// <remarks> Use to Get Service, return service if  all is fine
        /// NotFound if its is  not  registered
        /// </remarks>
        [HttpGet("{serviceId}")]
        [ProducesResponseType(typeof(Service), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetService([Range(1, int.MaxValue)] int serviceId, CancellationToken cancellationToken)
        {

            var service = await _serviceService.TakeService(serviceId, cancellationToken);
            if (service != null)
            {
                return Ok(service);
            }
            return NotFound("No client with that ID");
        }

        /// <summary>
        /// Add services 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.Conflict"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to create Service, return ok if all is fine
        /// Conflict if its is already registered</remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateService([FromBody] Service service, CancellationToken cancellationToken)
        {
            var serviceAdd = await _serviceService.AddService(service, cancellationToken);

            if (serviceAdd)
            {
                return Ok();
            }

            return Conflict();
        }

        /// <summary>
        /// Update services 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.NotFound"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to update service, send service with the same id
        /// and new fields, and it will be update
        /// Conflict if its is already registered</remarks>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateService([FromBody] Service service, CancellationToken cancellationToken)
        {
            var update = await _serviceService.UpdateService(service, cancellationToken);

            if (update)
            {
                return Ok();
            }

            return NotFound();
        }


        /// <summary>
        /// Delete services 
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> add service 
        /// <see cref="HttpStatusCode.NotFound"/> if services is added
        /// </returns>
        /// <remarks>
        /// Use to delete service
        /// Return ok if deleted and not found if BD have not this service
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteService([Range(1, int.MaxValue)] int serviceId, CancellationToken cancellationToken)
        {

            var deleted = await _serviceService.DeleteService(serviceId, cancellationToken);

            if (deleted)
            {
                return Ok();
            }

            return NotFound("No Service");
        }

    }
}

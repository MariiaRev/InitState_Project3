using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Show info about clients , Clients Login its his PhoneNumber ")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IWorkWithIdService _checkId;

        /// <summary>
        /// Constructor of client controller 
        /// </summary>
        public ClientController(IClientService clientService,IWorkWithIdService checkId)
        {
            _clientService = clientService;
            _checkId = checkId;
        }

        #region JS TILT

        

        
        ///// <summary>
        ///// return list of clients
        ///// </summary>
        ///// <param name="pageSize">The count of coaches to return at one time.</param>
        ///// <param name="page">The current page number.</param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/> if all is fine and return list of clients
        ///// <see cref="HttpStatusCode.NotFound"/> if no any clients
        ///// </returns>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpGet("{pageSize}/{page}")]
        //[ProducesResponseType(typeof(GetDataContract<Client>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetAllClients([FromRoute] int pageSize, [FromRoute] int page)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        /// <summary>
        /// return list of clients
        /// </summary>
        /// <param name="pageSize">The count of coaches to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> if all is fine and return list of clients
        /// <see cref="HttpStatusCode.NotFound"/> if no any clients
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.TakeAllClients();
            if (clients.Any())
            {
                return Ok(clients);
            }
            return NotFound("No elements");

        }

        /// <summary>
        /// return a client what admin need
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> if all is fine and return a clients
        /// <see cref="HttpStatusCode.NotFound"/> if no client with this Name
        /// <see cref="HttpStatusCode.BadRequest"/> if id is incorrect 
        /// </returns>
        /// <remarks>
        /// For get one client 
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClient(int id)
        {

            if (!_checkId.IsCorrectId(id))
            {
                return BadRequest("incorrect Id");
            }

            var client = await _clientService.TakeClient(id);

            if (client != null)
            {
                return Ok(client);
            }
            return NotFound("No client with that id");
        }

    }
}

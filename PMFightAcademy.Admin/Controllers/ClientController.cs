using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services;
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
        private readonly ClientService _clientService;

        /// <summary>
        /// Constructor of client controller 
        /// </summary>
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
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
            IEnumerable<Client> clients;
            try
            {
                clients = await _clientService.TakeAllClients();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(clients);

        }

        /// <summary>
        /// return a client what admin need
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> if all is fine and return a clients
        /// <see cref="HttpStatusCode.NotFound"/> if no client with this Name
        /// </returns>
        /// <remarks>
        /// For get one client 
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetClient(int id)
        {
            Client client;
            try
            {
                client = await _clientService.TakeClient(id);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

            return Ok(client);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;

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

        /// <summary>
        /// Constructor of client controller 
        /// </summary>
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        /// <summary>
        /// return list of clients
        /// </summary>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> if all is fine and return list of clients
        /// <see cref="HttpStatusCode.NotFound"/> if no any clients
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientContract>), (int)HttpStatusCode.OK)]
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
        /// <see cref="HttpStatusCode.NotFound"/> if no client with this id
        /// </returns>
        /// <remarks>
        /// For get one client 
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetClient([Range(1, int.MaxValue)] int id)
        {

            var client = await _clientService.TakeClient(id);

            if (client != null)
            {
                return Ok(client);
            }
            return NotFound("No client with that id");
        }

        /// <summary>
        /// Add description to client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> if all is fine and return Ok()
        /// <see cref="HttpStatusCode.NotFound"/> if no client with this id
        /// </returns>
        /// <remarks>
        /// for add description for client
        /// </remarks>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddClientDescription([Range(1, int.MaxValue)] int id, string description, CancellationToken cancellationToken)
        {
            var client = await _clientService.AddDescription(id, description, cancellationToken);

            if (client)
            {
                return Ok();
            }
            return NotFound("Description not added, or client not found");
        }
    }
}

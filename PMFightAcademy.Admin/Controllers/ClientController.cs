using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using PMFightAcademy.Admin.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("Show info about clients ")]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Constructor of client controller 
        /// </summary>
        public ClientController()
        {

        }

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
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(List<Client>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllClients([FromRoute] int pageSize, [FromRoute] int page)
        {
            throw new NotImplementedException();
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
        public async Task<Client> GetClient(int name)
        {
            throw new NotImplementedException();
        }
    }
}

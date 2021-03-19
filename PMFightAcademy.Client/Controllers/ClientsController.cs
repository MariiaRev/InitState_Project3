using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Client.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Client controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for registration, login and logout a client.")]
    public class ClientsController: ControllerBase
    {
        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="client">Client to register.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was succesffully registered.
        /// <see cref="HttpStatusCode.BadRequest"/> with <c>string</c> result message if <paramref name="client"/> data is invalid.
        /// <see cref="HttpStatusCode.Conflict"/> with <c>string</c> result message if <see cref="Models.Client.Login"/> already exists.
        /// </returns>
        /// <remarks>
        /// Returns OK if client was succesffully registered.
        /// Returns BadRequest if <paramref name="client"/> data is invalid.
        /// Returns Conflict if <paramref name="client"/> login already exists.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Register([FromBody]Models.Client client)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loggs in a registered client.
        /// </summary>
        /// <param name="loginContract">Contract for login action.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully logged in.
        /// <see cref="HttpStatusCode.BadRequest"/> with <c>string</c> result message if login or password are invalid.
        /// </returns>
        /// <remarks>
        /// Returns OK if client was successfully logged in.
        /// Returns BadRequest if login or password are invalid.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody]LoginContract loginContract)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loggs out a registered, logged in client.
        /// </summary>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully logged out.
        /// <see cref="HttpStatusCode.BadRequest"/> with <c>string</c> result message if cannot log out client because (s)he was not logged in.
        /// </returns>
        /// <remarks>
        /// Returns OK if client was successfully logged out.
        /// Returns BadRequest if cannot log out client because (s)he was not logged in.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }
    }
}

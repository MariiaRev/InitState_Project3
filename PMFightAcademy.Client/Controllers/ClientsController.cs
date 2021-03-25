using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Client controller.
    /// Formats of phone number:
    /// +38067 111 1111
    /// 38067 111 1111
    /// 067 111 1111
    /// Available country codes:
    /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
    /// Password must have at least 8 chars
    /// At least 1 upper char
    /// and at least 1 number
    /// </summary> 
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for registration and login a client.")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientsService _clientsService;

#pragma warning disable 1591
        public ClientsController(ILogger<ClientsController> logger, IClientsService clientsService)
        {
            _logger = logger;
            _clientsService = clientsService;
        }
#pragma warning restore 1591

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="model">Client to register.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully registered.
        /// <see cref="HttpStatusCode.BadRequest"/> if <paramref name="model"/> data is invalid.
        /// <see cref="HttpStatusCode.Conflict"/> if <see cref="Models.Client.Login"/> already exists.
        /// </returns>
        /// <remarks>
        /// Returns OK with
        /// <strong>json = { token: "jwt-token" }</strong>
        /// if client was successfully registered.
        /// Returns BadRequest if <paramref name="model"/> data is invalid.
        /// Returns Conflict if login already exists.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Models.Client model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _clientsService.Register(model);

                if (!string.IsNullOrEmpty(result))
                    return Ok(result);

                return Conflict();
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();

        }

        /// <summary>
        /// Log in in a registered client.
        /// </summary>
        /// <param name="model">Contract for login action.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> jwt-token if client was successfully logged in.
        /// <see cref="HttpStatusCode.BadRequest"/> if login or password are invalid.
        /// </returns>
        /// <remarks>
        /// Returns OK with
        /// <strong>json = { token: "jwt-token" }</strong>
        /// if client was successfully logged in.
        /// Returns BadRequest if login or password are invalid.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginContract model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _clientsService.Login(model);

                if (!string.IsNullOrEmpty(result))
                    return Ok(result);

                return BadRequest();
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();
        }
    }
}

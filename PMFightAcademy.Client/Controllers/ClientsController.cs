using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMFightAcademy.Client.Authorization;
using PMFightAcademy.Client.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Client.DataBase;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Client controller.
    /// Formats of phone number:
    /// +38067 111 1111
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
        private readonly ClientContext _context;

#pragma warning disable 1591
        public ClientsController(ILogger<ClientsController> logger, ClientContext context)
        {
            _logger = logger;
            _context = context;
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
                if (model.Login.StartsWith("+38"))
                    model.Login = new string(model.Login.Skip(3).ToArray());

                var user = _context.Clients.FirstOrDefault(m => m.Login == model.Login);
                if (user == null)
                {
                    user = new Models.Client
                    {
                        Login = model.Login,
                        Password = model.Password.GenerateHash(),
                        Name = model.Name
                    };

                    await _context.Clients.AddAsync(user, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);

                    user = _context.Clients.FirstOrDefault(m => m.Login == user.Login);

                    return Ok(Authenticate(user.Login, user.Id));
                }

                _logger.LogInformation($"{model.Login} is already exist");
                return Conflict();
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();

        }

        /// <summary>
        /// Loggs in a registered client.
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
        public IActionResult Login([FromBody] LoginContract model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (model.Login.StartsWith("+38"))
                    model.Login = new string(model.Login.Skip(3).ToArray());

                var user = _context.Clients.FirstOrDefault(m => m.Login == model.Login);

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return BadRequest();
                }

                if (model.Password.GenerateHash().Equals(user.Password))
                {
                    return Ok(Authenticate(user.Login, user.Id));
                }

                _logger.LogInformation($"{model.Login}:\tIncorrect login or password");
                return BadRequest();
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();
        }

        #region useless logout
        ///// <summary>
        ///// Loggs out a registered, logged in client.
        ///// </summary>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully logged out.
        ///// <see cref="HttpStatusCode.BadRequest"/> with <c>string</c> result message if cannot log out client because (s)he was not logged in.
        ///// </returns>
        ///// <remarks>
        ///// Returns OK if client was successfully logged out.
        ///// Returns BadRequest if cannot log out client because (s)he was not logged in.
        ///// </remarks>
        //[HttpPost("[action]")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Logout()
        //{
        //    return Ok();
        //}
        #endregion

        private string Authenticate(string userName, int id)
        {
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogInformation("userName in Authenticate can not be null");
                throw new ArgumentNullException(nameof(userName));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.UserData,  id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "ApplicationJWT", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromDays(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var json = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwt)
            };

            return JsonSerializer.Serialize(json);
        }
    }
}

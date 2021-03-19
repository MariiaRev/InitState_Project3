using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMFightAcademy.Client.Authorization;
using PMFightAcademy.Client.Contract;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Client controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for registration, login and logout a client.")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly ConcurrentBag<Models.Client> _bagUsers;

#pragma warning disable 1591
        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
            _bagUsers = new ConcurrentBag<Models.Client>();
        }
#pragma warning restore 1591

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="model">Client to register.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully registered.
        /// <see cref="HttpStatusCode.BadRequest"/> if <paramref name="model"/> data is invalid.
        /// <see cref="HttpStatusCode.Conflict"/> if <see cref="Models.Client.Login"/> already exists.
        /// </returns>
        /// <remarks>
        /// Returns OK if client was successfully registered.
        /// Returns BadRequest if <paramref name="model"/> data is invalid.
        /// Returns Conflict if <paramref name="model"/> login already exists.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [AllowAnonymous]
        public IActionResult Register([FromBody] Models.Client model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.Login == model.Login);

                if (user == null)
                {
                    _bagUsers.Add(new Models.Client
                    {
                        Login = model.Login,
                        Password = model.Password.GenerateHash(),
                        Name = model.Name
                    });

                    _bagUsers.TryPeek(out var m);

                    return Ok(Authenticate(m.Login, m.Id));
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
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/> with <c>string</c> result message if client was successfully logged in.
        /// <see cref="HttpStatusCode.BadRequest"/> if login or password are invalid.
        /// </returns>
        /// <remarks>
        /// Returns OK if client was successfully logged in.
        /// Returns BadRequest if login or password are invalid.
        /// </remarks>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginContract model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.Login == model.Login);

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return BadRequest();
                }

                if (model.Password.GenerateHash().Equals(user.Password))
                {
                    _bagUsers.TryPeek(out var m);

                    return Ok(Authenticate(m.Login, m.Id));
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

        private static string Authenticate(string userName, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.UserData,  id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromDays(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

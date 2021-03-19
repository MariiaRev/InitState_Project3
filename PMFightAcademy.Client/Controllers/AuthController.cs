using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMFightAcademy.Client.Authorization;
using PMFightAcademy.Client.Contract;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Client controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is for registration, login and logout a client.")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ConcurrentBag<User> _bagUsers;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            _bagUsers = new ConcurrentBag<User>();
        }

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
        public IActionResult Register([FromBody] LoginContract model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.PhoneNumber == model.Login);

                if (user == null)
                {
                    _bagUsers.Add(new User
                    {
                        PhoneNumber = model.Login,
                        Password = model.Password.GenerateHash()
                    });

                    return Ok(Authenticate(model.Login));
                }

                _logger.LogInformation($"{model.Login}:\tIncorrect login or password");
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();
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
        public IActionResult Login([FromBody] LoginContract model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.PhoneNumber == model.Login);

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return BadRequest();
                }

                if (model.Password.GenerateHash().Equals(user.Password)) 
                    return Ok(Authenticate(model.Login));

                _logger.LogInformation($"{model.Login}:\tIncorrect login or password");
                return BadRequest();
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();
        }

        private static string Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
            };

            return JsonSerializer.Serialize(response);
        }
    }
}

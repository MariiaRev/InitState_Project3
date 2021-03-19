using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMFightAcademy.Client.Authorization;

namespace PMFightAcademy.Client.Controllers
{
    [ApiController]
    [Route("account")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ConcurrentBag<User> _bagUsers;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            _bagUsers = new ConcurrentBag<User>();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegModel model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.PhoneNumber == model.PhoneNumber);

                if (user == null)
                {
                    _bagUsers.Add(new User
                    {
                        PhoneNumber = model.PhoneNumber,
                        Password = model.Password.GenerateHash()
                    });

                    return Ok(Authenticate(model.PhoneNumber));
                }

                _logger.LogInformation($"{model.PhoneNumber}:\tIncorrect login or password");
            }

            _logger.LogInformation("RegModel is not valid");
            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RegModel model)
        {
            if (model == null)
            {
                _logger.LogInformation("RegModel is null");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = _bagUsers.FirstOrDefault(m => m.PhoneNumber == model.PhoneNumber);

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return BadRequest();
                }

                if (model.Password.GenerateHash().Equals(user.Password)) 
                    return Ok(Authenticate(model.PhoneNumber));

                _logger.LogInformation($"{model.PhoneNumber}:\tIncorrect login or password");
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

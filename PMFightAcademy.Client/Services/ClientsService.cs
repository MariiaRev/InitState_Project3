using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMFightAcademy.Client.Authorization;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Client.Contract.Dto;
using static PMFightAcademy.Client.Mappings.ClientMapping;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for the ClientsController
    /// </summary>
    public class ClientsService : IClientsService
    {
        private readonly ILogger<ClientsService> _logger;
        private readonly ApplicationContext _context;
#pragma warning disable 1591
        public ClientsService(ILogger<ClientsService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
#pragma warning restore 1591

        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="model"><see cref="ClientDto"/> client to register.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> Register(ClientDto model, CancellationToken token)
        {
            if (model.Login.StartsWith("38"))
                model.Login = new string(model.Login.Skip(2).ToArray());

            if (model.Login.StartsWith("+38"))
                model.Login = new string(model.Login.Skip(3).ToArray());

            var user = await _context.Clients.FirstOrDefaultAsync(m => m.Login == model.Login, token);

            if (user == null)
            {
                user = ClientDtoToClient(model);

                await _context.Clients.AddAsync(user, token);

                await _context.SaveChangesAsync(token);

                _logger.LogInformation($"Client {model.Login} is added");

                user = await _context.Clients.FirstOrDefaultAsync(m => m.Login == model.Login, token);

                return Authenticate(user.Login, user.Id);
            }

            _logger.LogInformation($"Client {model.Login} is already exist");
            return string.Empty;
        }

        /// <summary>
        /// Log in in a registered client.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> Login(LoginContract model, CancellationToken token)
        {
            if (model.Login.StartsWith("38"))
                model.Login = new string(model.Login.Skip(2).ToArray());

            if (model.Login.StartsWith("+38"))
                model.Login = new string(model.Login.Skip(3).ToArray());

            var user = await _context.Clients.FirstOrDefaultAsync(m => m.Login == model.Login, token);

            if (user == null)
            {
                _logger.LogInformation($"Client {model.Login} not found");
                return string.Empty;
            }

            if (model.Password.GenerateHash().Equals(user.Password))
            {
                return Authenticate(user.Login, user.Id);
            }

            _logger.LogInformation($"{model.Login}:\tIncorrect login or password");
            return string.Empty;
        }

        /// <summary>
        /// Private method for the creating of jwt-token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string Authenticate(string userName, int id)
        {
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogInformation("userName in Authenticate can not be null");
                throw new ArgumentNullException(nameof(userName));
            }

            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, userName),
                new(ClaimTypes.UserData,  id.ToString())
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

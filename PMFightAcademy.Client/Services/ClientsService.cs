using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
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
        /// <param name="client"><see cref="ClientDto"/> client to register.</param>
        /// <returns></returns>
        public Task<string> Register(ClientDto client)
        {
            var model = ClientDtoToClient(client);

            if (model.Login.StartsWith("38"))
                model.Login = new string(model.Login.Skip(2).ToArray());

            if (model.Login.StartsWith("+38"))
                model.Login = new string(model.Login.Skip(3).ToArray());

            var user = _context.Clients.FirstOrDefault(m => m.Login == model.Login);

            if (user == null)
            {
                user = new Dal.Models.Client
                {
                    Login = model.Login,
                    Password = model.Password.GenerateHash(),
                    Name = model.Name
                };

                _context.Clients.Add(user);

                _context.SaveChanges();

                user = _context.Clients.FirstOrDefault(m => m.Login == user.Login);

                return Task.FromResult(Authenticate(user.Login, user.Id));
            }

            _logger.LogInformation($"{model.Login} is already exist");
            return Task.FromResult(string.Empty);
        }

        /// <summary>
        /// Log in in a registered client.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> Login(LoginContract model)
        {
            if (model.Login.StartsWith("38"))
                model.Login = new string(model.Login.Skip(2).ToArray());

            if (model.Login.StartsWith("+38"))
                model.Login = new string(model.Login.Skip(3).ToArray());

            var user = _context.Clients.FirstOrDefault(m => m.Login == model.Login);

            if (user == null)
            {
                _logger.LogInformation("User not found");
                return Task.FromResult(string.Empty);
            }

            if (model.Password.GenerateHash().Equals(user.Password))
            {
                return Task.FromResult(Authenticate(user.Login, user.Id));
            }

            _logger.LogInformation($"{model.Login}:\tIncorrect login or password");
            return Task.FromResult(string.Empty);
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

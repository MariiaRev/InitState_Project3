﻿using Microsoft.Extensions.Logging;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using PMFightAcademy.Dal.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static PMFightAcademy.Admin.Mapping.ClientMapping;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Client Service
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public ClientService(ILogger<ClientService> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take coaches
        /// </summary>
        public async Task<IEnumerable<ClientContract>> TakeAllClients()
        {
            var clients = _dbContext.Clients
                .Select(cl => ClientMapFromModelToContract(cl))
                .AsEnumerable()
                .OrderBy(x => x.Name);

            return clients;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        public async Task<ClientContract> TakeClient(int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            return ClientMapFromModelToContract(client);
        }

        /// <summary>
        /// Add desc
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="desc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> AddDescription(int clientId, string desc, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId,cancellationToken);
            if (client == null)
            {
                _logger.LogInformation($"Client with id {clientId} is not found");
                return false;
            }

            client.Description = desc;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
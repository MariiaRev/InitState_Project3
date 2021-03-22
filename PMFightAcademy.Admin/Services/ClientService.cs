using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Client Service
    /// </summary>
    public class ClientService: IClientService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public ClientService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Take coaches
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<Client>> TakeAllClients()
        {
            var clients = _dbContext.Clients;
            return clients.AsEnumerable();
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client> TakeClient(int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            return client;
        }
    }
}
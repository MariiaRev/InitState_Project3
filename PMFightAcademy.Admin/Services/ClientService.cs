using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Client Service
    /// </summary>
    public class ClientService
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
            var clients = _dbContext.Clients.ToList();

            if (clients.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }

            return clients.ToList();
        }

        /// <summary>
        /// Take Coache
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Client> TakeClient(int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            if (client == null)
                throw new ArgumentException();
            return client;
        }
    }
}
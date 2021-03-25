using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Client Service
    /// </summary>
    public class ClientService : IClientService
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
        public async Task<IEnumerable<Client>> TakeAllClients()
        {
            var clients = _dbContext.Clients;
            return clients.AsEnumerable();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        public async Task<Client> TakeClient(int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            return client;
        }

        /// <summary>
        /// Add desc
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public async Task<bool> AddDescription(int clientId, string desc, CancellationToken cancellationToken)
        {
            var client = _dbContext.Clients.FirstOrDefault(x => x.Id == clientId);
            if (client == null) return false;
            client.Description = desc;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
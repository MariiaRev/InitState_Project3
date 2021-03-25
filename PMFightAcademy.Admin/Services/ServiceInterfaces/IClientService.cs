using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Client Service
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Take all clients 
        /// </summary>
        /// <returns></returns>
        public  Task<IEnumerable<Client>> TakeAllClients();

        /// <summary>
        /// Take client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public  Task<Client> TakeClient(int clientId);

        /// <summary>
        /// Add Description 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public  Task<bool> AddDescription(int clientId, string desc, CancellationToken cancellationToken);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using PMFightAcademy.Admin.DataBase;
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
    }
}
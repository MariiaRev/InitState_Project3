using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Interface for the Client Service 
    /// </summary>
    public interface IClientsService
    {
        /// <summary>
        /// Registers a new client.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> Register(Models.Client model);

        /// <summary>
        /// Log in in a registered client.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> Login(Models.Client model);

        /// <summary>
        /// Private method for the creating of jwt-token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        string Authenticate(string userName, int id);
    }
}

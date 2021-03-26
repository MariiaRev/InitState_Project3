using System.Threading.Tasks;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;

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
        Task<string> Register(ClientDto model);

        /// <summary>
        /// Log in in a registered client.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> Login(LoginContract model);
    }
}

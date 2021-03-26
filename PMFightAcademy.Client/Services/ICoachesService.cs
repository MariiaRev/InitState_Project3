using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Dal.Models;
using System.Threading.Tasks;
using System.Threading;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Coaches storage service abstraction.
    /// </summary>
    public interface ICoachesService
    {
        /// <summary>
        /// Gets coaches list with paggination.
        /// </summary>
        /// <param name="pageSize">Coaches count per page.</param>
        /// <param name="page">Current page.</param>
        /// <param name="token">Cancellation token for DB requests.</param>
        /// <param name="filter">Optional filter for searching corresponding coaches (by first or last name).</param>
        /// <returns>Returns list of found coaches or empty list if there is no coach.</returns>
        Task<GetDataContract<CoachDto>> GetCoaches(int pageSize, int page, CancellationToken token, string filter = null);
    }
}

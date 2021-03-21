using PMFightAcademy.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Coaches storage service abstraction.
    /// </summary>
    public interface ICoachesStorageService
    {
        /// <summary>
        /// Gets coaches list with paggination.
        /// </summary>
        /// <param name="skipCount">How many coaches to skip.</param>
        /// <param name="takeCount">How many coaches to take.</param>
        /// <param name="filter">Optional filter for searching corresponding coaches (by first or last name).</param>
        /// <returns>Returns list of found coaches or empty list if there is no coach.</returns>
        IEnumerable<Coach> GetCoaches(int skipCount, int takeCount, string filter = null);
    }
}

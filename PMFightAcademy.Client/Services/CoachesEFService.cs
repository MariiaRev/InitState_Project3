using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System.Collections.Generic;
using System.Linq;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for getting coaches from database with Entity Framework.
    /// </summary>
    public class CoachesEFService : ICoachesStorageService
    {
        private readonly ClientContext _dbContext;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        public CoachesEFService(ClientContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public IEnumerable<Coach> GetCoaches(int skipCount, int takeCount, string filter = null)
        {            
            if (!string.IsNullOrWhiteSpace(filter))
            {
                return _dbContext.Coaches.
                    Where(coach => coach.FirstName.Contains(filter) || coach.LastName.Contains(filter))
                    .Skip(skipCount).Take(takeCount);
            };

            var coaches = _dbContext.Coaches.Skip(skipCount).Take(takeCount).ToList();
            return coaches;
        }
    }
}

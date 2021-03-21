using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Client.DataBase;

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
        public IEnumerable<Coach> GetCoaches(int skipCount, int takeCount)
        {
            return _dbContext.Coaches.Skip(skipCount).Take(takeCount);
        }
    }
}

﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// CoachService
    /// </summary>
    public interface ICoachService
    {
        /// <summary>
        /// Take all coaches
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<CoachContract>> TakeAllCoaches();
        /// <summary>
        /// Take coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public  Task<CoachContract> TakeCoach(int coachId);

        /// <summary>
        /// create coach
        /// </summary>
        /// <param name="coachContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task AddCoach(CoachContract coachContract,CancellationToken cancellationToken);

        /// <summary>
        /// Delete coach
        /// </summary>
        /// <param name="coachContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task<bool> DeleteCoach(CoachContract coachContract, CancellationToken cancellationToken);

        /// <summary>
        /// Update coach 
        /// </summary>
        /// <param name="coachContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task UpdateCoach(CoachContract coachContract, CancellationToken cancellationToken);

    }
}
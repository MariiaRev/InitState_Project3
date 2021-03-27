using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Qualification service
    /// </summary>
    public interface IQualificationService
    {
        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteQualification(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Add Qualification
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> AddQualification(QualificationContract contract, CancellationToken cancellationToken);

        /// <summary>
        /// Get coaches for service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<CoachContract>> GetCoachesForService(int serviceId, CancellationToken cancellationToken);
        /// <summary>
        /// Get services for coaches 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<Service>> GetServicesForCoach(int coachId, CancellationToken cancellationToken);
    }
}
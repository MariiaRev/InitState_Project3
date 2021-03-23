using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;

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
        /// <param name="contract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task<bool> DeleteQualification(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Add Qualification
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task AddQualification(QualificationContract contract, CancellationToken cancellationToken);
        /// <summary>
        /// Get coaches for service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public  Task<IEnumerable<CoachContract>> GetCoachesForService(int serviceId);
        /// <summary>
        /// Get services for coaches 
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public  Task<IEnumerable<Service>> GetServicesForCoach(int coachId);
    }
}
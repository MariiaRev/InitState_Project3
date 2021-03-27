using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Dal.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service 
    /// </summary>
    public class QualificationService : IQualificationService
    {
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public QualificationService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;

        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> DeleteQualification(int id, CancellationToken cancellationToken)
        {
            var qualification = _dbContext.Qualifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (qualification == null)
            {
                return false;
            }
            try
            {
                _dbContext.Remove(qualification);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> AddQualification(QualificationContract contract, CancellationToken cancellationToken)
        {

            try
            {
                var qualification = QualificationMapping.QualificationMapFromContractToModel(contract);
                await _dbContext.AddAsync(qualification, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Get coaches
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="token"></param>
        public async Task<IEnumerable<CoachContract>> GetCoachesForService(int serviceId, CancellationToken token)
        {
            var qualifications = await _dbContext.Qualifications.ToListAsync(token);

            var coaches = qualifications.Where(x => x.ServiceId == serviceId).Select(x => x.Coach);
            var coachesContracts = coaches.AsEnumerable().Select(CoachMapping.CoachMapFromModelToContract);

            return coachesContracts;
        }
        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="token"></param>
        public async Task<IEnumerable<Service>> GetServicesForCoach(int coachId, CancellationToken token)
        {
            var qualifications = await _dbContext.Qualifications.ToListAsync(token);
            var services = _dbContext.Qualifications.Where(x => x.CoachId == coachId).Select(x => x.Service);
            return services.AsEnumerable();
        }

        /// <summary>
        /// Qualifications for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QualificationContract>> GetQualificationsForCoach(int coachId, CancellationToken cancellationToken)
        {
            var qualifications = await _dbContext.Qualifications.Where(x => x.CoachId == coachId)
                .ToListAsync(cancellationToken);
            return qualifications.Select(QualificationMapping.QualificationMapFromModelToContract);
        }

        /// <summary>
        /// Qualifications for Service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QualificationContract>> GetQualificationsForService(int serviceId, CancellationToken cancellationToken)
        {
            var qualifications = await _dbContext.Qualifications.Where(x => x.ServiceId == serviceId)
                .ToListAsync(cancellationToken);
            return qualifications.Select(QualificationMapping.QualificationMapFromModelToContract);
        }
    }
}
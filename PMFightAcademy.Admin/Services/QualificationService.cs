using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service 
    /// </summary>
    public class QualificationService : IQualificationService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public QualificationService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> DeleteQualification(int id, CancellationToken cancellationToken)
        {
            var qualification = _dbContext.Qualifications.FirstOrDefault(x => x.Id == id);
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
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddQualification(QualificationContract contract, CancellationToken cancellationToken)
        {
            try
            {
                var qualification = QualificationMapping.QualificationMapFromContractToModel(contract);
                await _dbContext.AddAsync(qualification, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                throw new ArgumentException("No coach");
            }

        }

        /// <summary>
        /// Get coaches
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<CoachContract>> GetCoachesForService(int serviceId)
        {

            var coaches = _dbContext.Qualifications.Where(x => x.ServiceId == serviceId).Select(x => x.Coach);
            var coachesContracts = coaches.AsEnumerable().Select(CoachMapping.CoachMapFromModelToContract);

            return coachesContracts;
        }
        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<Service>> GetServicesForCoach(int coachId)
        {

            var services = _dbContext.Qualifications.Where(x => x.CoachId == coachId).Select(x => x.Service);
            return services.AsEnumerable();

        }

    }
}
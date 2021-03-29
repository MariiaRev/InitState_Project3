using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Dal.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service 
    /// </summary>
    public class QualificationService : IQualificationService
    {
        private readonly ILogger<QualificationService> _logger;
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public QualificationService(ILogger<QualificationService> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Delete qualification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> DeleteQualification(int id, CancellationToken cancellationToken)
        {
            var qualification =await _dbContext.Qualifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (qualification == null)
            {
                _logger.LogInformation($"Qualification with id {id} is not found");
                return false;
            }
            try
            {
                _dbContext.Remove(qualification);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Qualification with id {id} is not removed");
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
            var checkQualification = await _dbContext.Qualifications.FirstOrDefaultAsync(x =>
                x.CoachId == contract.CoachId && x.ServiceId == contract.ServiceId,cancellationToken);
            if (checkQualification != null)
            {
                _logger.LogInformation($"Qualification with {contract.CoachId} and {contract.ServiceId}  is found");
                return false;
            }
            try
            {
                var qualification = QualificationMapping.QualificationMapFromContractToModel(contract);
                await _dbContext.AddAsync(qualification, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Qualification with id {contract.Id} is not found");
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

            var result = _dbContext.Qualifications
                .Where(x => x.ServiceId == serviceId).Select(x => x.Coach).AsEnumerable()
                .Select(CoachMapping.CoachMapFromModelToContract);

            return result;
        }
        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="token"></param>
        public async Task<IEnumerable<Service>> GetServicesForCoach(int coachId, CancellationToken token)
        {
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
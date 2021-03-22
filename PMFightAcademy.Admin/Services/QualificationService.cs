using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service 
    /// </summary>
    public class QualificationService
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
        public async Task DeleteQualification(QualificationContract contract)
        {
            try
            {
                var qualification = QualificationMapping.QualificationMapFromContractToModel(contract);
                _dbContext.Remove(qualification);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("No coach");
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddQualification(QualificationContract contract)
        {
            try
            {
                var qualification = QualificationMapping.QualificationMapFromContractToModel(contract);
                await _dbContext.AddAsync(qualification);
                await _dbContext.SaveChangesAsync();
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
            IEnumerable<CoachContract> coachesContracts;
            try
            {
                var coaches = _dbContext.Qualifications.Where(x => x.ServiceId == serviceId).Select(x => x.Coach).ToList();
                coachesContracts = coaches.Select(CoachMapping.CoachMapFromModelToContract).ToList();
            }
            catch (Exception e)
            {
                throw new ArgumentException();
            }

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
            IEnumerable<Service> services;
            try
            {
                 services = _dbContext.Qualifications.Where(x => x.CoachId == coachId).Select(x => x.Service).ToList();
            }
            catch (Exception e)
            {
                throw new ArgumentException();
            }

            return services;


        }

    }
}
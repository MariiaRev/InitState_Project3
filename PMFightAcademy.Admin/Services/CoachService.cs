using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Coach Service
    /// </summary>
    public class CoachService : ICoachService
    {
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public CoachService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take coaches
        /// </summary>
        public async Task<IEnumerable<CoachContract>> TakeAllCoaches()
        {
            var coaches = _dbContext.Coaches;

            return coaches.AsEnumerable().Select(CoachMapping.CoachMapFromModelToContract);
        }

        /// <summary>
        /// Take Coach
        /// </summary>s
        /// <param name="coachId"></param>
        public async Task<CoachContract> TakeCoach(int coachId)
        {
            var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachId);
            return CoachMapping.CoachMapFromModelToContract(coach);
        }

        /// <summary>
        /// Add coach
        /// </summary>
        /// <param name="coachContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddCoach(CoachContract coachContract, CancellationToken cancellationToken)
        {
            var checkCoach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachContract.Id);

            if (checkCoach!=null)
            {
                return false;
            }

            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);

            var age = (int)(DateTime.Now.Subtract(coach.BirthDate).TotalDays / 365.2425);

            if (age < 18 || age > 90)
                return false;

            try
            {
                await _dbContext.Coaches.AddAsync(coach, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> DeleteCoach(int id, CancellationToken cancellationToken)
        {
            var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == id);
            if (coach == null)
            {
                return false;
            }
            try
            {
                _dbContext.Remove(coach);
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
        /// <param name="coachContract"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> UpdateCoach(CoachContract coachContract, CancellationToken cancellationToken)
        {

            var checkCoach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachContract.Id);

            if (checkCoach == null)
            {
                return false;
            }

            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);

            var age = (int)(DateTime.Now.Subtract(coach.BirthDate).TotalDays / 365.2425);

            if (age < 18 || age > 90)
                return false;

            try
            {
                _dbContext.Update(coach);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }
            return true;

        }
    }
}
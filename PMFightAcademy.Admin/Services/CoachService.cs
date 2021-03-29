using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Coach Service
    /// </summary>
    public class CoachService : ICoachService
    {
        private readonly ILogger<CoachService> _logger;
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public CoachService(ILogger<CoachService> logger, ApplicationContext dbContext)
        {
            _logger = logger;
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
        /// <param name="cancellationToken"></param>
        public async Task<CoachContract> TakeCoach(int coachId,CancellationToken cancellationToken)
        {
            var coach = await _dbContext.Coaches.FirstOrDefaultAsync(x => x.Id == coachId, cancellationToken);
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
            var checkCoach = await _dbContext.Coaches.FirstOrDefaultAsync(x => x.Id == coachContract.Id,cancellationToken);

            if (checkCoach!=null)
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not found");
                return false;
            }

            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);

            var age = (int)(DateTime.Now.Subtract(coach.BirthDate).TotalDays / 365.2425);

            if (age < 18 || age > 90)
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not suitable for age : {age}");
                return false;
            }

            try
            {
                await _dbContext.Coaches.AddAsync(coach, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not added");
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
            var coach = await _dbContext.Coaches.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            if (coach == null)
            {
                _logger.LogInformation($"Coach with id {id} is not suitable for age");
                return false;
            }
            try
            {
                _dbContext.Remove(coach);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Coach with id {id} is not removed");
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

            var checkCoach = await _dbContext.Coaches.FirstOrDefaultAsync(x => x.Id == coachContract.Id,cancellationToken);

            if (checkCoach == null)
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not found");
                return false;
            }

            var coach = CoachMapping.CoachMapFromContractToModel(coachContract,checkCoach);

            var age = (int)(DateTime.Now.Subtract(coach.BirthDate).TotalDays / 365.2425);



            if (age < 18 || age > 90)
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not suitable for age : {age}");
                return false;
            }

            try
            {
                _dbContext.Coaches.Update(coach);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogInformation($"Coach with id {coachContract.Id} is not updated");
                return false;
            }
            return true;

        }
    }
}
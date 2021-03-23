using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Coach Service
    /// </summary>
    public class CoachService : ICoachService
    {
        private readonly AdminContext _dbContext;
        private readonly IWorkWithIdService _newId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="newId"></param>
        public CoachService(AdminContext dbContext,IWorkWithIdService newId)
        {
            _dbContext = dbContext;
            _newId = newId;
        }

        /// <summary>
        /// Take coaches
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<CoachContract>> TakeAllCoaches()
        {
            var coaches = _dbContext.Coaches;

            return coaches.AsEnumerable().Select(CoachMapping.CoachMapFromModelToContract);
        }

        /// <summary>
        /// Take Coache
        /// </summary>s
        /// <param name="coachId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<CoachContract> TakeCoach(int coachId)
        {
            var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachId);
            return CoachMapping.CoachMapFromModelToContract(coach);
        }

        /// <summary>
        /// Add coach
        /// </summary>
        /// <param name="coachContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddCoach(CoachContract coachContract, CancellationToken cancellationToken)
        {
            coachContract.Id = _newId.GetIdForCoach();

            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);
            try
            {
                await _dbContext.Coaches.AddAsync(coach, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                throw new ArgumentException("Coach is already registered");
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="coachContract"></param>
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
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateCoach(CoachContract coachContract, CancellationToken cancellationToken)
        {
            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);
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
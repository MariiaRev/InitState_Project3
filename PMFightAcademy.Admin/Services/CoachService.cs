﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Coach Service
    /// </summary>
    public class CoachService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public CoachService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take coaches
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<CoachContract>> TakeAllCoaches()
        {
            var coaches = _dbContext.Coaches.ToList();

            if (coaches.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }

            return coaches.Select(CoachMapping.CoachMapFromModelToContract).ToList();
        }

        /// <summary>
        /// Take Coache
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<CoachContract> TakeCoach(int coachId)
        {
            var coach = _dbContext.Coaches.FirstOrDefault(x => x.Id == coachId);
            if (coach == null)
                throw new ArgumentException();
            return CoachMapping.CoachMapFromModelToContract(coach);
        }

        /// <summary>
        /// Add coach
        /// </summary>
        /// <param name="coachContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddCoach(CoachContract coachContract)
        {
            var coach = CoachMapping.CoachMapFromContractToModel(coachContract);
            try
            {
                await _dbContext.Coaches.AddAsync(coach);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="coachContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteCoach(CoachContract coachContract)
        {
            
            try
            {
                var coach = CoachMapping.CoachMapFromContractToModel(coachContract);
                _dbContext.Remove(coach);
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
        /// <param name="coachContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task UpdateCoach(CoachContract coachContract)
        {
            try
            {
                var coach = CoachMapping.CoachMapFromContractToModel(coachContract);
                _dbContext.Update(coach);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("No coach");
            }

        }
    }
}
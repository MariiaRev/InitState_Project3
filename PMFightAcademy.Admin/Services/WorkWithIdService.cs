using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Work with ID
    /// </summary>
    public class WorkWithIdService: IWorkWithIdService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Work with ID
        /// </summary>
        /// <param name="dbContext"></param>
        public  WorkWithIdService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Check Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  bool IsCorrectId(int id)
        {
            return id >= 0;
        }

        /// <summary>
        /// RebaseId
        /// </summary>
        
        /// <param name="allId"></param>
        /// <returns></returns>
        private static int ReturnNewId(ICollection<int> allId)
        {
            if(!allId.Any())
                return 0;
            for (var i = 1; i <= allId.Count; i++)
            {
                if (!allId.Contains(i))
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Create new ID
        /// </summary>
        /// <returns></returns>
        public int GetIdForBooking()
        {
            var newId = _dbContext.Coaches.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }

        /// <summary>
        /// Create new ID
        /// </summary>
        /// <returns></returns>
        public int GetIdForCoach()
        {
            var newId = _dbContext.Coaches.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }

        /// <summary>
        /// Create new ID
        /// </summary>
        /// <returns></returns>
        public int GetIdForQualification()
        {
            var newId = _dbContext.Coaches.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }

        /// <summary>
        /// Create new ID
        /// </summary>
        /// <returns></returns>
        public int GetIdForService()
        {
            var newId = _dbContext.Coaches.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }

        /// <summary>
        /// Create new ID
        /// </summary>
        /// <returns></returns>
        public int GetIdForClient()
        {
            var newId = _dbContext.Coaches.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetIdForSlots()
        {
            var newId = _dbContext.Slots.Select(x => x.Id).ToList();
            return ReturnNewId(newId);
        }
    }
}
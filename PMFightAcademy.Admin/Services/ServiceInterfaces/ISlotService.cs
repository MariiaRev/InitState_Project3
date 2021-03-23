using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Interface slot service
    /// </summary>
    public interface ISlotService
    {
        /// <summary>
        /// Add slots
        /// </summary>
        /// <param name="slotContract"></param>
        /// <returns></returns>
        public  Task AddSlot(SlotsCreateContract slotContract, CancellationToken cancellationToken);
        /// <summary>
        /// Remove slots
        /// </summary>
        /// <param name="slotContract"></param>
        /// <returns></returns>
        public  Task<bool> RemoveSlot(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Take all slots
        /// </summary>
        /// <returns></returns>
        public  Task<IEnumerable<SlotsReturnContract>> TakeAllSlots();
        /// <summary>
        /// Take slots for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public  Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoach(int coachId);
        /// <summary>
        /// Take all on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public  Task<IEnumerable<SlotsReturnContract>> TakeAllOnDate(string date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoachOnDates(int coachId, string start,
            string end);


    }
}
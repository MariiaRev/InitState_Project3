using System;
using PMFightAcademy.Admin.Contract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Interface slot service
    /// </summary>
    public interface ISlotService
    {

        public Task<bool> AddListOfSlots(IEnumerable<SlotsReturnContract> slotsArray, CancellationToken cancellationToken);
        
        //public Task AddSlotRange(SlotsCreateContract slotContract, CancellationToken cancellationToken);
        /// <summary>
        /// Remove slots
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemoveSlot(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Take all slots
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SlotsReturnContract>> TakeAllSlots();

        /// <summary>
        /// update
        /// </summary>
        /// <param name="slotContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateSlot(SlotsReturnContract slotContract, CancellationToken cancellationToken);
        /// <summary>
        /// Take slots for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoach(int coachId);
        /// <summary>
        /// Take all on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<IEnumerable<SlotsReturnContract>> TakeAllOnDate(string date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoachOnDates(int coachId, string start,
            string end);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> RemoveSlotRange(IEnumerable<int> arrayId, CancellationToken cancellationToken);

    }
}
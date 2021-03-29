using System;
using PMFightAcademy.Admin.Contract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Dal.Models;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Interface slot service
    /// </summary>
    public interface ISlotService
    {

        /// <summary>
        /// Add list of slots
        /// </summary>
        /// <param name="slotsArray"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> AddListOfSlots(IEnumerable<SlotsReturnContract> slotsArray, CancellationToken cancellationToken);
        
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
        /// Remove slots 
        /// </summary>
        /// <param name="arrayId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> RemoveSlotRange(IEnumerable<int> arrayId, CancellationToken cancellationToken);


        /// <summary>
        /// From Date to date take slots for coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Obsolete]
        public Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoachOnDates(int coachId, string start,
            string end);

        /// <summary>
        /// Add slots in range from - to
        /// </summary>
        /// <param name="slotContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete]
        public Task AddSlotRange(SlotsCreateContract slotContract, CancellationToken cancellationToken);
        /// <summary>
        /// Remove slots
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public Task<bool> RemoveSlot(int id, CancellationToken cancellationToken);
        [Obsolete]
        public  Task<GetDataContract<SlotsReturnContract>> TakeSlotsForCoach(int coachId, int pageSize, int page);
        [Obsolete]
        public  Task<GetDataContract<SlotsReturnContract>> TakeAllOnDate(DateTime date, int pageSize, int page);
        [Obsolete]
        public  Task<GetDataContract<SlotsReturnContract>> TakeAllSlots(int pageSize, int page);

    }
}
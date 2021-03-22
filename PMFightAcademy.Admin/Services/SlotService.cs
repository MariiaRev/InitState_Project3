using System;
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
    /// Slot Service
    /// </summary>
    public class SlotService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="dbContext"></param>
        public SlotService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creation slots 
        /// </summary>
        /// <param name="slotContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddSlot(SlotsCreateContract slotContract)
        {
            var slot = SlotsMapping.SlotMapFromContractToModel(slotContract);

            List<Slot> slots = new List<Slot>();
            while (slot.StartTime<=slot.Duration)
            {
                var resultSlot = new Slot
                {
                    CoachId = slot.CoachId,
                    Duration = TimeSpan.FromHours(1),
                    Date = slot.Date,
                    StartTime = slot.StartTime,

                };
                slot.StartTime = slot.StartTime + resultSlot.Duration;
                slots.Add(resultSlot);
            }
            try
            {
              await _dbContext.AddRangeAsync(slots);
              await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Remove slots 
        /// </summary>
        /// <param name="slotContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task RemoveSlot(SlotsCreateContract slotContract)
        {
            var slot = SlotsMapping.SlotMapFromContractToModel(slotContract);
            try
            {
                _dbContext.Slots.Remove(slot);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        #region Maded Pagination but not used by JS (TILT)
        /// <summary>
        /// Take all slots 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetDataContract<SlotsCreateContract>> TakeAllSlots(int pageSize, int page)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Invalid pages");

            var slots = _dbContext.Slots.ToArray();

            if (slots.Length <= 0)
            {
                throw new ArgumentException("No elements");
            }
            var slotsPerPages = slots.Skip((page - 1) * pageSize).Take(pageSize).ToArray();


            var pagination = new Paggination()
            {
                Page = page,
                TotalPages = (int) Math.Ceiling((decimal) slots.Length / pageSize)
            };
            var data = new GetDataContract<SlotsCreateContract>()
            {
                Data = slotsPerPages.Select(SlotsMapping.SlotMapFromModelToContract).ToArray(),
                Paggination = pagination
            };
            return data;
        }

        /// <summary>
        /// Take list of slots for coach 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetDataContract<SlotsCreateContract>> TakeSlotsForCoach(int coachId,int pageSize, int page)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Invalid pages");

            //var slots = _dbContext.Slots.AsParallel().Where(x=>x.CoachId == coachId).ToArray();
            var slots = _dbContext.Slots.Where(x => x.CoachId == coachId).ToArray();

            if (slots.Length <= 0)
            {
                throw new ArgumentException("No elements");
            }

            var slotsPerPages = slots.Skip((page - 1) * pageSize).Take(pageSize).ToArray();


            var pagination = new Paggination()
            {
                Page = page,
                TotalPages = (int)Math.Ceiling((decimal)slots.Length / pageSize)
            };
            var data = new GetDataContract<SlotsCreateContract>()
            {
                Data = slotsPerPages.Select(SlotsMapping.SlotMapFromModelToContract).ToArray(),
                Paggination = pagination
            };
            return data;
        }
        /// <summary>
        /// Take all slots on date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetDataContract<SlotsCreateContract>> TakeAllOnDate(DateTime date,int pageSize, int page)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Invalid changes");

            //var slots = _dbContext.Slots.AsParallel().Where(x => x.Date == date).ToArray();

            var slots = _dbContext.Slots.Where(x=>x.Date == date).ToArray();

            if (slots.Length <= 0)
            {
                throw new ArgumentException("No elements");
            }

            var slotsPerPages = slots.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            var pagination = new Paggination()
            {
                Page = page,
                TotalPages = (int)Math.Ceiling((decimal)slots.Length / pageSize)
            };
            var data = new GetDataContract<SlotsCreateContract>()
            {
                Data = slotsPerPages.Select(SlotsMapping.SlotMapFromModelToContract).ToArray(),
                Paggination = pagination
            };
            return data;
        }

        #endregion


        /// <summary>
        /// Take all slots
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<SlotsCreateContract>> TakeAllSlots()
        {
            var slots = _dbContext.Slots.ToList();

            if (slots.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }

            return slots.Select(SlotsMapping.SlotMapFromModelToContract).ToList();
        }

        /// <summary>
        /// Take all slots for coaches
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<SlotsCreateContract>> TakeSlotsForCoach(int coachId)
        {
            var slots = _dbContext.Slots.Where(x => x.CoachId == coachId).ToList();
            if (slots.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }
            return slots.Select(SlotsMapping.SlotMapFromModelToContract).ToList();
        }
        /// <summary>
        /// Take all slots on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<SlotsCreateContract>> TakeAllOnDate(DateTime date)
        {
            var slots = _dbContext.Slots.Where(x => x.Date == date).ToList();
            if (slots.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }
            return slots.Select(SlotsMapping.SlotMapFromModelToContract).ToList();
        }
    }
}
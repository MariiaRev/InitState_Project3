using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Slot Service
    /// </summary>
    public class SlotService : ISlotService
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddSlot(SlotsCreateContract slotContract, CancellationToken cancellationToken)
        {
            var slot = new Slot();
            try
            {
                slot = SlotsMapping.SlotMapFromContractToModel(slotContract);
            }
            catch
            {
                throw new ArgumentException();
            }
            

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
              await _dbContext.AddRangeAsync(slots, cancellationToken);
              await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                throw new ArgumentException();
            }
        }


        /// <summary>
        /// Remove slots 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool> RemoveSlot(int id, CancellationToken cancellationToken)
        {
            var slot = _dbContext.Slots.FirstOrDefault(x => x.Id == id);
            if (slot == null)
            {
                return false;
            }
            try
            {
                _dbContext.Remove(slot);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #region Maded Pagination but not used by JS (TILT)
        /// <summary>
        /// Take all slots 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetDataContract<SlotsReturnContract>> TakeAllSlots(int pageSize, int page)
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
            var data = new GetDataContract<SlotsReturnContract>()
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
        public async Task<GetDataContract<SlotsReturnContract>> TakeSlotsForCoach(int coachId,int pageSize, int page)
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
            var data = new GetDataContract<SlotsReturnContract>()
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
        public async Task<GetDataContract<SlotsReturnContract>> TakeAllOnDate(DateTime date,int pageSize, int page)
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
            var data = new GetDataContract<SlotsReturnContract>()
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
        public async Task<IEnumerable<SlotsReturnContract>> TakeAllSlots()
        {
            
            var slots = _dbContext.Slots.Select(SlotsMapping.SlotMapFromModelToContract);
            
            return slots.AsEnumerable();
        }

        /// <summary>
        /// Take all slots for coaches
        /// </summary>
        /// <param name="coachId"></param>
        public async Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoach(int coachId)
        {
            var slots = _dbContext.Slots.Where(x => x.CoachId == coachId);
            
            return slots.AsEnumerable().Select(SlotsMapping.SlotMapFromModelToContract);
        }
        /// <summary>
        /// Take all slots on date
        /// </summary>
        /// <param name="date"></param>
        public async Task<IEnumerable<SlotsReturnContract>> TakeAllOnDate(string date)
        {
            if (!DateTime.TryParseExact(date, "MM.dd.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var dateStart))
                return new List<SlotsReturnContract>();

            var slots = _dbContext.Slots.Where(x => x.Date == dateStart);
            
            return slots.AsEnumerable().Select(SlotsMapping.SlotMapFromModelToContract);
        }

        /// <summary>
        /// Take all slots for coaches
        /// </summary>
        /// <param name="coachId">Coach id</param>
        /// <param name="start">Date start</param>
        /// <param name="end">Date to </param>
        public async Task<IEnumerable<SlotsReturnContract>> TakeSlotsForCoachOnDates(int coachId, string start, string end)
        {
            //var test = DateTime.ParseExact(start, "MM/dd/yyyy",CultureInfo.InvariantCulture);
            if (!DateTime.TryParseExact(start, "MM.dd.yyyy", CultureInfo.CurrentCulture ,DateTimeStyles.None, out var dateStart))
                return new List<SlotsReturnContract>();
            if (!DateTime.TryParseExact(end, "MM.dd.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out var dateEnd))
                return new List<SlotsReturnContract>();

            var slots = _dbContext.Slots.Select(x=>x).Where(x => x.CoachId == coachId).Where(x=>x.Date >= dateStart).Where(x=>x.Date<=dateEnd);

            return slots.AsEnumerable().Select(SlotsMapping.SlotMapFromModelToContract);
        }


    }
}
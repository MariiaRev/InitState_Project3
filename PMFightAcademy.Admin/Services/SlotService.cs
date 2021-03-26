using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.DataBase;
using PMFightAcademy.Admin.Mapping;
using PMFightAcademy.Dal;
using PMFightAcademy.Dal.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Slot Service
    /// </summary>
    public class SlotService : ISlotService
    {
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="dbContext"></param>
        public SlotService(ApplicationContext dbContext)
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

                if (slot.StartTime > slot.Duration)
                    throw new ArgumentException();
            }
            catch
            {
                throw new ArgumentException();
            }

            var timeEnd = slot.Duration;


            if (_dbContext.Slots.Where(x => x.Date == slot.Date)
                .Where(x => x.StartTime <= slot.Duration).Where(x => x.CoachId == slot.CoachId).Any(x => x.StartTime >= slot.StartTime))
            {
                throw new ArgumentException("some slots created in this time range , for this coach");
            }

            var slots = new List<Slot>();

            while (slot.StartTime <= timeEnd)
            {
                var resultSlot = new Slot
                {
                    CoachId = slot.CoachId,
                    Duration = TimeSpan.FromHours(1),
                    Date = slot.Date,
                    StartTime = slot.StartTime,

                };
                slots.Add(resultSlot);
                slot.StartTime += resultSlot.Duration;
            }

            try
            {
                
                await _dbContext.AddRangeAsync(slots, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                //await _dbContext.AddAsync(resultSlot, cancellationToken);
                //await _dbContext.SaveChangesAsync(cancellationToken);
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
        /// Take list of slots for coach 
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GetDataContract<SlotsReturnContract>> TakeSlotsForCoach(int coachId, int pageSize, int page)
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
        public async Task<GetDataContract<SlotsReturnContract>> TakeAllOnDate(DateTime date, int pageSize, int page)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Invalid changes");

            //var slots = _dbContext.Slots.AsParallel().Where(x => x.Date == date).ToArray();

            var slots = _dbContext.Slots.Where(x => x.Date == date).ToArray();

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
            //var slots = SlotsMapping.SlotMapFromModelToContractNewSlotsJS(_dbContext.Slots);

            var slots = _dbContext.Slots.Select(SlotsMapping.SlotMapFromModelToContract);

            return slots.AsEnumerable();
        }


        /// <summary>
        /// update coach
        /// </summary>
        /// <param name="slotContract"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateSlot(SlotsCreateContract slotContract, CancellationToken cancellationToken)
        {
            var slot = SlotsMapping.SlotMapFromContractToModel(slotContract);

            if (slot.StartTime > slot.Duration)
                return false;

            try
            {
                _dbContext.Update(slot);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }
            return true;
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
            if (!DateTime.TryParseExact(date, Settings.DateFormat, null, DateTimeStyles.None, out var dateStart))
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
            //var test = DateTime.ParseExact(start, Settings.DateFormat, CultureInfo.InvariantCulture);
            if (!DateTime.TryParseExact(start, Settings.DateFormat, null, DateTimeStyles.None, out var dateStart))
                return new List<SlotsReturnContract>();
            if (!DateTime.TryParseExact(end, Settings.DateFormat, null, DateTimeStyles.None, out var dateEnd))
                return new List<SlotsReturnContract>();

            var slots = _dbContext.Slots.Select(x => x).Where(x => x.CoachId == coachId).Where(x => x.Date >= dateStart).Where(x => x.Date <= dateEnd);

            return slots.AsEnumerable().Select(SlotsMapping.SlotMapFromModelToContract);
        }
    }
}
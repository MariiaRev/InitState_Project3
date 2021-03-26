using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal;
using PMFightAcademy.Dal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// Slots mapping 
    /// </summary>
    public static class SlotsMapping
    {
        /// <summary>
        /// from contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Slot SlotMapFromContractToModel(SlotsCreateContract contract)
        {
            if (contract == null)
            {
                return null;
            }
            return new Slot
            {
                Id = contract.Id,
                CoachId = contract.CoachId,
                Date = DateTime.ParseExact(contract.DateStart, Settings.DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None),
                Duration = TimeSpan.Parse(contract.TimeEnd, CultureInfo.CurrentCulture),
                StartTime = TimeSpan.Parse(contract.TimeStart, CultureInfo.CurrentCulture)
            };

        }

        /// <summary>
        /// From model to Contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SlotsReturnContract SlotMapFromModelToContract(Slot model)
        {
            if (model == null)
            {
                return null;
            }
            return new SlotsReturnContract
            {
                Id = model.Id,
                CoachId = model.CoachId,
                DateStart = model.Date.ToString(Settings.DateFormat),
                Duration = (new DateTime(1, 1, 1) + model.Duration).ToString(Settings.TimeFormat),
                TimeStart = (new DateTime(1, 1, 1) + model.StartTime).ToString(Settings.TimeFormat)
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slots"></param>
        /// <returns></returns>
        public static IEnumerable<SlotsCreateContract> SlotMapFromModelToContractNewSlotsJS(IEnumerable<Slot> slots)
        {

            List<SlotsCreateContract> result = new List<SlotsCreateContract>();

            List<List<Slot>> bigSlotsList = new List<List<Slot>>();
            var dates = slots.Select(x => x.Date).Distinct();
            var coaches = slots.Select(x => x.CoachId).Distinct();

            foreach (var date in dates)
            {
                foreach (var coach in coaches)
                {
                    var slotsList = slots.Where(x => x.Date == date).Where(x => x.CoachId == coach).ToList();
                    bigSlotsList.Add(slotsList);
                }
            }

            var count = 0;
            foreach (var item in bigSlotsList)
            {
                var resultItem = item.FirstOrDefault();
                var lastTime = item.Max(x => x.StartTime);
                var startTime = item.Min(x => x.StartTime);
                result.Add(new SlotsCreateContract()
                {
                    Id = count,
                    CoachId = resultItem.CoachId,
                    DateStart = resultItem.Date.ToString(Settings.DateFormat),
                    TimeEnd = (new DateTime(1, 1, 1) + lastTime).ToString(Settings.TimeFormat),
                    TimeStart = (new DateTime(1, 1, 1) + startTime).ToString(Settings.TimeFormat)
                });
                count++;
            }

            return result;
        }
    }
}
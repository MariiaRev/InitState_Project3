using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
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
                Date = DateTime.ParseExact(contract.DateStart, Settings.DateFormat, null, DateTimeStyles.None),
                Duration = TimeSpan.Parse(contract.TimeEnd, null),
                StartTime = TimeSpan.Parse(contract.TimeStart, null)
            };

        }
        /// <summary>
        /// Return contract
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Slot SlotMapFromContractToModel(SlotsReturnContract contract)
        {
            if (contract == null)
            {
                return null;
            }
            return new Slot
            {
                Id = contract.Id,
                CoachId = contract.CoachId,
                Date = DateTime.ParseExact(contract.DateStart, Settings.DateFormat, null, DateTimeStyles.None),
                Duration = TimeSpan.Parse(contract.Duration, null),
                StartTime = TimeSpan.Parse(contract.TimeStart, null)
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



    }
}
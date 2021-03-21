using System;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// Slots mapping 
    /// </summary>
    public static class SlotsMapping
    {
        //private readonly AdminContext _dbContext;

        //public SlotsMapping(AdminContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        /// <summary>
        /// from contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Slot SlotMapFromContractToModel(SlotsCreateContract contract)
        {
            
            return new Slot
            {
                CoachId = contract.CoachId,
                Date = DateTime.Parse(contract.DateStart),
                Duration = TimeSpan.Parse(contract.TimeEnd),
                StartTime = TimeSpan.Parse(contract.TimeStart)
            };
            
        }

        /// <summary>
        /// From model to Contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SlotsCreateContract SlotMapFromModelToContract(Slot model)
        {
            return new SlotsCreateContract
            {
                Id = model.Id,
                CoachId = model.CoachId,
                DateStart = model.Date.ToString("MM/dd/yyyy"),
                TimeEnd = model.Duration.ToString("HH:mm"),
                TimeStart = model.StartTime.ToString("HH:mm")
            };
           
        }
    }
}
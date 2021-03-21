using System;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// Coach Mapping
    /// </summary>
    public static class CoachMapping
    {
        /// <summary>
        /// From contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Coach CoachMapFromContractToModel(CoachContract contract)
        {
            return new Coach()
            {
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                BirthDate = DateTime.Parse(contract.DateBirth),
                Description = contract.Description,
                PhoneNumber = contract.PhoneNumber
            };
        }

        /// <summary>
        /// From model to Contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CoachContract CoachMapFromModelToContract(Coach model)
        {
            return new CoachContract()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateBirth = model.BirthDate.ToString("MM/dd/yyyy"),
                Description = model.Description,
                PhoneNumber = model.PhoneNumber
            };
        }

    }
}
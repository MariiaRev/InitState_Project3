using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
using System;
using System.Globalization;

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
            if (contract == null)
            {
                return null;
            }
            return new Coach()
            {
                Id = contract.Id,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                BirthDate = DateTime.ParseExact(contract.DateBirth, "MM.dd.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None),
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
            if (model == null)
            {
                return null;
            }
            return new CoachContract()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateBirth = model.BirthDate.ToString("MM.dd.yyyy"),
                Description = model.Description,
                PhoneNumber = model.PhoneNumber
            };
        }

    }
}
﻿using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// QualificationMapping
    /// </summary>
    public static class QualificationMapping
    {
        /// <summary>
        /// From Contract to Model
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public static Qualification QualificationMapFromContractToModel(QualificationContract contract)
        {
            return new Qualification
            {
                CoachId = contract.CoachId,
                ServiceId = contract.ServiceId
            };
        }
        /// <summary>
        /// From model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static QualificationContract QualificationMapFromModelToContract(Qualification model)
        {
            return new QualificationContract
            {
                Id = model.Id,
                CoachId = model.CoachId,
                ServiceId = model.ServiceId
            };
        }
    }
}
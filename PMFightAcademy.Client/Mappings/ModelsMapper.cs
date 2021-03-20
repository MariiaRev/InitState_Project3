using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Models;
using System;

namespace PMFightAcademy.Client.Mappings
{
    /// <summary>
    /// Converts models from <see cref="Models"/> to  their other
    /// matching models from <see cref="Contract.Dto"/>.
    /// </summary>
    public static class ModelsMapper
    {
        /// <summary>
        /// Converts <see cref="Coach"/> model to the <see cref="CoachDto"/> one.
        /// </summary>
        /// <param name="coach"><paramref name="coach"/> to convert.</param>
        /// <returns>Converted <see cref="CoachDto"/> coach.</returns>
        public static CoachDto CoachToCoachDto(Coach coach)
        {
            return new CoachDto()
            {
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                Age = GetAgeByDate(coach.BirthDate),
                Description = coach.Description,
                PhoneNumber = coach.PhoneNumber
            };
        }

        private static int GetAgeByDate(DateTime date)
        {
            return (int)(DateTime.Now.Subtract(date).TotalDays / 365.2425);
        }
    }
}

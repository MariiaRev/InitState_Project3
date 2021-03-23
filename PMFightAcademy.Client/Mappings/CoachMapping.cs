using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;

namespace PMFightAcademy.Client.Mappings
{
    /// <summary>
    /// Converts <see cref="Coach"/> to <see cref="CoachDto"/>.
    /// </summary>
    public static class CoachMapping
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
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                Age = GetAgeByDate(coach.BirthDate),
                Description = coach.Description,
                PhoneNumber = coach.PhoneNumber
            };
        }

        /// <summary>
        /// Converts <see cref="Coach"/> model to the <see cref="CoachDto"/> one.
        /// </summary>
        /// <param name="coach"><paramref name="coach"/> to convert.</param>
        /// <param name="services">Services which can be provided by the coach.</param>
        /// <returns>Converted <see cref="CoachDto"/> coach.</returns>
        public static CoachDto CoachWithServicesToCoachDto(Coach coach, IEnumerable<string> services)
        {
            return new CoachDto()
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                Age = GetAgeByDate(coach.BirthDate),
                Description = coach.Description,
                PhoneNumber = coach.PhoneNumber,
                Services = services ?? new List<string>()
            };
        }

        private static int GetAgeByDate(DateTime date)
        {
            return (int)(DateTime.Now.Subtract(date).TotalDays / 365.2425);
        }
    }
}

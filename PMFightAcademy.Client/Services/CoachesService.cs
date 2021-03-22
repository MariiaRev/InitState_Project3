using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System.Collections.Generic;
using System.Linq;
using static PMFightAcademy.Client.Mappings.CoachMapping;
using System;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for getting coaches from database with Entity Framework.
    /// </summary>
    public class CoachesService : ICoachesService
    {
        private readonly ClientContext _dbContext;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        public CoachesService(ClientContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public GetDataContract<CoachDto> GetCoaches(int pageSize, int page, string filter = null)
        {
            IEnumerable<Coach> coaches = _dbContext.Coaches;

            // filter coaches
            if (!string.IsNullOrWhiteSpace(filter))
            {
                coaches = coaches
                   .Where(coach => coach.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                   coach.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase));
            };

            // get total coaches count
            var coachesCount = (decimal)coaches.Count();

            // skip and take coaches
            coaches = coaches.Skip((page - 1) * pageSize).Take(pageSize);

            // return intricate data object
            return new GetDataContract<CoachDto>()
            {
                Data = coaches.Select(coach => CoachWithServicesToCoachDto(coach,
                    coach.Qualifications?.Select(q => q.Service?.Name))),
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(coachesCount / pageSize)
                }
            };
        }
    }
}

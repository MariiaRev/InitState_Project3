using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static PMFightAcademy.Client.Mappings.CoachMapping;

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
        public async Task<GetDataContract<CoachDto>> GetCoaches(int pageSize, int page, CancellationToken token, string filter = null)
        {
            var coaches = await _dbContext.Coaches.Include(coach => coach.Qualifications).ToListAsync(token);

            // filter coaches
            var filteredCoaches = coaches.Where(c => filter == null ||
                                                     ContainsIgnoreCase(c.FirstName, filter) ||
                                                     ContainsIgnoreCase(c.LastName, filter));
            
            // get total coaches count
            var coachesCount = (decimal)filteredCoaches.Count();

            if (coachesCount == 0)
            {
                return new GetDataContract<CoachDto>()
                {
                    Data = new List<CoachDto>().AsEnumerable(),
                    Paggination = new Paggination()
                };
            }

            // skip and take coaches
            var resultCoaches = filteredCoaches.Skip((page - 1) * pageSize).Take(pageSize);

            // get services
            IEnumerable<Qualification> qualifications = _dbContext.Qualifications.Include(q => q.Service);

            // combine result
            var result = resultCoaches.Select(coach => CoachWithServicesToCoachDto(coach,
                    qualifications.Where(q => q.CoachId == coach.Id).Select(q => q.Service.Name)));

            // return intricate data object
            return new GetDataContract<CoachDto>()
            {
                Data = result,
                Paggination = new Paggination()
                {
                    Page = page,
                    TotalPages = (int)Math.Ceiling(coachesCount / pageSize)
                }
            };
        }

        private static bool ContainsIgnoreCase(string field, string filter)
        {
            return field.Contains(filter, StringComparison.OrdinalIgnoreCase);
        }
    }
}
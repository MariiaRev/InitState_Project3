using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Client.Contract;
using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.DataBase;
using PMFightAcademy.Client.Mapping;
using PMFightAcademy.Client.Models;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Service for booking controller
    /// </summary>
    public class BookingService
    {
        private readonly ClientContext _context;

#pragma warning disable 1591
        public BookingService(ClientContext context)
        {
            _context = context;
        }
#pragma warning restore 1591

        public Task<List<Service>> GetServicesForBooking()
        {
            var result = _context.Services.ToList();
            if (result.Count != 0)
                return Task.FromResult(result);

            throw new ArgumentException("Service Collection is empty");
        }
    }
}

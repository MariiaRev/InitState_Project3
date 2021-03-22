using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service service
    /// </summary>
    public class ServiceService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public ServiceService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Take All
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<Service>> TakeAllServices()
        {
            var services = _dbContext.Services.ToList();

            if (services.Count <= 0)
            {
                throw new ArgumentException("No elements");
            }

            return services.ToList();
        }

        /// <summary>
        /// Take
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Service> TakeService(int serviceId)
        {
            var service = _dbContext.Services.FirstOrDefault(x => x.Id == serviceId);
            if (service == null)
                throw new ArgumentException();
            return service;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddService(Service service)
        {
            try
            {
               await _dbContext.Services.AddAsync(service);
               await _dbContext.SaveChangesAsync();
            }
            catch 
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteService(Service service)
        {
            try
            {
                _dbContext.Remove(service);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("No Service");
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task UpdateService(Service service)
        {
            try
            {
                _dbContext.Services.Update(service);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("No Service");
            }

        }

        



    }
}
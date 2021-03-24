using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Models;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Service service
    /// </summary>
    public class ServiceService :IServiceService
    {
        private readonly AdminContext _dbContext;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="newId"></param>
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
            var services = _dbContext.Services;
            return services.AsEnumerable();
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
            return service;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddService(Service service, CancellationToken cancellationToken)
        {
            //service.Id = _newId.GetIdForService();
            try
            {
               await _dbContext.Services.AddAsync(service, cancellationToken);
               await _dbContext.SaveChangesAsync(cancellationToken);
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
        public async Task<bool> DeleteService(int id, CancellationToken cancellationToken)
        {
            var service = _dbContext.Services.FirstOrDefault(x => x.Id == id);
            if (service == null)
            {
                return false;
            }
            try
            {
                _dbContext.Remove(service);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateService(Service service, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Update(service);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return false;
            }

            return true;

        }

        



    }
}
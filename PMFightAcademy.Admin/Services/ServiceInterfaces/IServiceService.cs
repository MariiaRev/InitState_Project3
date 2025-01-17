﻿using PMFightAcademy.Dal.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Interface service 
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Take all service
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Service>> TakeAllServices();

        /// <summary>
        /// Take service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Service> TakeService(int serviceId,CancellationToken cancellationToken);

        /// <summary>
        /// Add service
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> AddService(Service service, CancellationToken cancellationToken);

        /// <summary>
        /// Delete services
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteService(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateService(Service service, CancellationToken cancellationToken);
    }
}
﻿using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Dal.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Client.Services
{
    /// <summary>
    /// Booking service abstraction.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Get available services for client booking for Booking Controller.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Service>> GetServicesForBooking(CancellationToken token);

        /// <summary>
        /// Get available services for client booking for Booking Controller with pagination.
        /// </summary>
        /// <returns></returns>
        Task<GetDataContract<Service>> GetServicesForBooking(int pageSize, int page, CancellationToken token);

        /// <summary>
        /// Get available coaches which can provide service with id <paramref name="serviceId"/> for Booking Controller  
        /// </summary>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="token"></param>
        /// <returns>List of coaches of type <see cref="CoachDto"/></returns>
        Task<IEnumerable<CoachDto>> GetCoachesForBooking(int serviceId, CancellationToken token);

        /// <summary>
        /// Get available coaches which can provide service with id <paramref name="serviceId"/> for Booking Controller  
        /// </summary>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="pageSize">Coaches count per page.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of coaches of type <see cref="CoachDto"/></returns>
        Task<GetDataContract<CoachDto>> GetCoachesForBooking(int serviceId, int pageSize, int page, CancellationToken token);

        /// <summary>
        /// Get available dates in format "MM.dd.yyyy" to provide a service with id <paramref name="serviceId"/> by coach with id <paramref name="coachId"/>
        /// for Booking Controller
        /// </summary>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="coachId">Id of the coach.</param>
        /// <param name="token"></param>
        /// <returns>List of available dates each as a string in format "MM.dd.yyyy".</returns>
        Task<IEnumerable<string>> GetDatesForBooking(int serviceId, int coachId, CancellationToken token);

        /// <summary>
        /// Get available time slots in format "HH:mm" to provide a service with id <paramref name="serviceId"/> for Booking Controller
        /// </summary>
        /// <param name="serviceId">Id of the service.</param>
        /// <param name="coachId">Id of the coach.</param>
        /// <param name="date">Selected date as a string in format "MM.dd.yyyy".</param>
        /// <param name="token"></param>
        /// <returns>List of available time slots in format "HH:mm".</returns>
        Task<IEnumerable<string>> GetTimeSlotsForBooking(int serviceId, int coachId, string date, CancellationToken token);

        /// <summary>
        /// Adds a booking for Booking Controller.
        /// </summary>
        /// <param name="bookingDto">Data for booking.</param>
        /// <param name="clientId">Id of the client who books a service.</param>
        /// <param name="token"></param>
        Task<bool> AddBooking(BookingDto bookingDto, int clientId, CancellationToken token);

        /// <summary>
        /// Gets active bookings of services.
        /// </summary>
        /// <param name="pageSize">The count of active booking records to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="clientId">Id of the client for which to return active bookings.</param>
        /// <param name="token">Cancellation token for DB requests.</param>
        /// <returns>
        /// Returns list of active bookings with paggination or 
        /// empty list if there is no record for active booking.
        /// </returns>
        Task<GetDataContract<HistoryDto>> GetActiveBookings(int pageSize, int page, int clientId, CancellationToken token);

        /// <summary>
        /// Gets booking history.
        /// </summary>
        /// <param name="pageSize">The count of booking history records to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="clientId">Id of the client for which to return the booking history.</param>
        /// <param name="token">Cancellation token for DB requests.</param>
        /// <returns>
        /// Returns list of the booking history with paggination or 
        /// empty list if there is no record for the booking history.
        /// </returns>
        Task<GetDataContract<HistoryDto>> GetBookingHistory(int pageSize, int page, int clientId, CancellationToken token);
    }
}

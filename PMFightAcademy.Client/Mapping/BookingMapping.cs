using PMFightAcademy.Client.Contract.Dto;
using PMFightAcademy.Client.Models;

namespace PMFightAcademy.Client.Mapping
{
    /// <summary>
    /// Mapping for Book
    /// </summary>
    public static class BookingMapping
    {
        /// <summary>
        /// From Contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="slotId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static Booking BookingMapFromContractToModel(BookingDto contract, int slotId, int clientId)
        {
            return new Booking()
            {
               ServiceId = contract.ServiceId,
               SlotId = slotId,
               ClientId = clientId
            };
        }

        /// <summary>
        /// From model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public static BookingDto CoachMapFromModelTToContract(Booking model, string date, string time, int coachId)
        {
            return new BookingDto()
            {
                Date = date,
                Time = time,
                ServiceId = model.ServiceId,
                CoachId = coachId
            };
        }
    }
}
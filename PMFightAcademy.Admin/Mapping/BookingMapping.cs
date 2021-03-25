using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Mapping
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
        /// <returns></returns>
        public static Booking BookingMapFromContractToModel(BookingContract contract)
        {
            if (contract == null)
            {
                return null;
            }
            return new Booking()
            {
                Id = contract.Id, 
                SlotId = contract.SlotId, 
                ServiceId = contract.ServiceId, 
                ClientId = contract.ClientId,
                ResultPrice = contract.ResultPrice
            };
        }

        /// <summary>
        /// From model to contract
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BookingContract BookingMapFromModelTToContract(Booking model)
        {
            if (model == null)
            {
                return null;
            }
            return new BookingContract()
            {
                Id = model.Id,
                ClientId = model.ClientId,
                ServiceId = model.ServiceId,
                SlotId = model.SlotId,
                ResultPrice = model.ResultPrice
            };
        }
    }
}
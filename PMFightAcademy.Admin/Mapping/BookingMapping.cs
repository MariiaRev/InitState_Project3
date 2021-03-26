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
        /// <param name="returnContractram>
        /// <returns></returns>
        public static Booking BookingMapFromContractToModel(BookingReturnContract returnContract)
        {
            if (returnContract == null)
            {
                return null;
            }
            return new Booking()
            {
                Id = returnContract.Id,
                SlotId = returnContract.Slot.Id,
                ServiceId = returnContract.ServiceId,
                ClientId = returnContract.ClientId,
                ResultPrice = returnContract.ResultPrice
            };
        }

        /// <summary>
        /// From model to returnContract
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BookingReturnContract BookingMapFromModelTToContract(Slot slot ,Booking model)
        {
            if (model == null)
            {
                return null;
            }
            return new BookingReturnContract()
            {
                Id = model.Id,
                ClientId = model.ClientId,
                ServiceId = model.ServiceId,
                Slot =SlotsMapping.SlotMapFromModelToContract(slot),
                ResultPrice = model.ResultPrice
            };
        }
    }
}
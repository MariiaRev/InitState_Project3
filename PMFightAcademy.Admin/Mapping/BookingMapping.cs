using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.Models;

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
        /// <param name="returnContract"></param>
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
        /// update mapp
        /// </summary>
        /// <param name="returnContract"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        //public static Booking BookingMapFromUpdateContractToModel(BookingUpdateContract returnContract, Booking model)
        //{
        //    if (returnContract == null)
        //    {
        //        return null;
        //    }
        //    return new Booking()
        //    {
        //        Id = returnContract.Id,
        //        SlotId = returnContract.SlotId,
        //        Slot = model.Slot,
        //        ServiceId = returnContract.ServiceId,
        //        ClientId = returnContract.ClientId,
        //        ResultPrice = returnContract.ResultPrice
        //    };
        //}

        /// <summary>
        /// From model to returnContract
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BookingReturnContract BookingMapFromModelTToContract(Slot slot, Booking model)
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
                Slot = SlotsMapping.SlotMapFromModelToContract(slot),
                ResultPrice = model.ResultPrice
            };
        }

        //public static BookingUpdateContract BookingMapFromModelTToUpdateContract(Booking model)
        //{
        //    if (model == null)
        //    {
        //        return null;
        //    }
        //    return new BookingUpdateContract()
        //    {
        //        Id = model.Id,
        //        ClientId = model.ClientId,
        //        ServiceId = model.ServiceId,
        //        SlotId = model.SlotId,
        //        ResultPrice = model.ResultPrice
        //    };
        //}
    }
}
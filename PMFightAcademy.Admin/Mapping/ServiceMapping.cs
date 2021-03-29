using PMFightAcademy.Dal.Models;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// Service mapping
    /// </summary>
    public class ServiceMapping
    {
        /// <summary>
        /// From contract to model
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Service ServiceMapFromModelToModel(Service contract, Service model )
        {
            if (model == null)
            {
                return null;
            }

            model.Description = contract.Description;
            model.Name = contract.Name;
            model.Price = contract.Price;
            return model;
        }
    }
}
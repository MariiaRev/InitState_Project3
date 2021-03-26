using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Dal.Models;

namespace PMFightAcademy.Admin.Mapping
{
    /// <summary>
    /// Maps <see cref="Client"/> and <see cref="ClientContract"/>.
    /// </summary>
    public static class ClientMapping
    {
        /// <summary>
        /// Converts <see cref="Client"/> model to the <see cref="ClientContract"/> one.
        /// </summary>
        /// <param name="client"><see cref="Client"/> client to convert.</param>
        /// <returns>Converted <see cref="ClientContract"/> client.</returns>
        public static ClientContract ClientMapFromModelToContract(Client client)
        {
            return new ClientContract()
            {
                Id = client.Id,
                Name = client.Name,
                Login = client.Login,
                Description = client.Description
            };
        }
    }
}

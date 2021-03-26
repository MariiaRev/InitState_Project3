using PMFightAcademy.Client.Contract.Dto;

namespace PMFightAcademy.Client.Mappings
{
    /// <summary>
    /// Converts <see cref="Dal.Models.Client"/> to <see cref="ClientDto"/>.
    /// </summary>
    public static class ClientMapping
    {
        /// <summary>
        /// Converts <see cref="ClientDto"/> model to the <see cref="Dal.Models.Client"/> one.
        /// </summary>
        /// <param name="client"><see cref="ClientDto"/> <paramref name="client"/> to convert.</param>
        /// <returns>Converted <see cref="Dal.Models.Client"/> client.</returns>
        public static Dal.Models.Client ClientDtoToClient(ClientDto client)
        {
            return new Dal.Models.Client()
            {
                Login = client.Login,
                Password = client.Password,
                Name = client.Name
            };
        }
    }
}

using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Contract for getting data with paggination.
    /// </summary>
    /// <typeparam name="T">The type of result data.</typeparam>
    public class GetDataContract<T> where T: class
    {
        /// <summary>
        /// The array of <typeparamref name="T"/> elements.
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// Paggination state.
        /// </summary>
        public Paggination Paggination { get; set; }
    }
}

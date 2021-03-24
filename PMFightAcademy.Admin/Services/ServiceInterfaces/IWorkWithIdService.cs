namespace PMFightAcademy.Admin.Services.ServiceInterfaces
{
    /// <summary>
    /// Work with Id
    /// </summary>
    public interface IWorkWithIdService
    {
        /// <summary>
        /// Correct id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsCorrectId(int id);
      
    }
}
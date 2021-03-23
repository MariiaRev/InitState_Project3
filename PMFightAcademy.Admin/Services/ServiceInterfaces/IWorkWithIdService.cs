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
        /// <summary>
        /// Id for booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdForBooking();
        /// <summary>
        /// Id for coach
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdForCoach();
        /// <summary>
        /// Id for qualifications
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdForQualification();
        /// <summary>
        /// id for service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdForService();
        /// <summary>
        /// Id for client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdForClient();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetIdForSlots();

    }
}
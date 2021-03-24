using PMFightAcademy.Admin.DataBase;
using PMFightAcademy.Admin.Services.ServiceInterfaces;

namespace PMFightAcademy.Admin.Services
{
    /// <summary>
    /// Work with ID
    /// </summary>
    public class WorkWithIdService : IWorkWithIdService
    {
        private readonly AdminContext _dbContext;

        /// <summary>
        /// Work with ID
        /// </summary>
        /// <param name="dbContext"></param>
        public WorkWithIdService(AdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Check Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsCorrectId(int id)
        {
            return id >= 0;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PMFightAcademy.Admin.DataBase
{
    public class AdminContext:DbContext
    {
        

        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        
    }
}

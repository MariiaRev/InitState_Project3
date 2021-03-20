using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Client.Models;

namespace PMFightAcademy.Client.DataBase
{
    public class ClientContext :DbContext
    {

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slot> Slots { get; set; }


        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }

    }
}
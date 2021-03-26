using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Client.Models;

namespace PMFightAcademy.Client.DataBase
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ClientContext :DbContext
    {
        public DbSet<Dal.Models.Client> Clients { get; set; }
        public DbSet<Dal.Models.Coach> Coaches { get; set; }
        public DbSet<Dal.Models.Booking> Bookings { get; set; }
        public DbSet<Dal.Models.Qualification> Qualifications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slot> Slots { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
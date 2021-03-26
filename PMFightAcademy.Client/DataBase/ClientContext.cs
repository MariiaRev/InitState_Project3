using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Client.Models;

namespace PMFightAcademy.Client.DataBase
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ClientContext :DbContext
    {
        public virtual DbSet<Models.Client> Clients { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
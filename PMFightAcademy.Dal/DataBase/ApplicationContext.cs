using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Dal.Models;

namespace PMFightAcademy.Dal.DataBase
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }

        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=db-postgresql-fra1-03704-prod-do-user-8904989-0.b.db.ondigitalocean.com;Port=25060;Database=stagedb;Username=stage;Password=ejsqm3u8zpngy50l;SslMode=Require;trustServerCertificate=true;ApplicationName=PMFA");
        }
    }
}

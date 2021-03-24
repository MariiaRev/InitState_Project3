using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace PMFightAcademy.Admin.DataBase
{
    public class AdminContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public AdminContext()
        {

        }
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Coach>().ToTable("Coaches");
            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<Qualification>().ToTable("Qualifications");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<Slot>().ToTable("Slots");
        }


    }
}

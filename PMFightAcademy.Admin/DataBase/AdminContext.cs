using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.DataBase
{
    public class AdminContext:DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slot> Slots { get; set; }
        


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

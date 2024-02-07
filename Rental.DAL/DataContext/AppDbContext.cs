using Microsoft.EntityFrameworkCore;
using Rental.DAL.Entities;

namespace Rental.DAL.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Motorcycle> Motorcycle { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Rent> Rent { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rent>().OwnsOne(c => c.RentPlan, plan =>
            {
                plan.Property(e => e.Days).HasColumnName("Days");
                plan.Property(e => e.Price).HasColumnName("Price");
                plan.Property(e => e.Fee).HasColumnName("Fee");
            });
        }
    }

}


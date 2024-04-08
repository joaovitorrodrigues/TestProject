using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Entities;

namespace TestProject.Infrastructure.Context
{
    public class TestProjectDbContext : DbContext
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public TestProjectDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Motorcycle>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<SubscriptionPlan>();
            modelBuilder.Ignore<UserLoginInfo>();
            modelBuilder.Entity<Booking>();
            modelBuilder.Entity<User>();
        }
    }
}

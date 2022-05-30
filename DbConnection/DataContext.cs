using DbConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbConnection
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (!options.IsConfigured)
            {

                options.UseSqlServer("data source=LOCALHOST\\SQlEXPRESS; initial catalog=PathProject;user id=PathProject_user;password=123456.;");

            }

        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>()
               .HasMany(c => c.Carts)
               .WithOne(e => e.Account);

            modelBuilder.Entity<Carts>()
               .HasMany(c => c.CartItems)
               .WithOne(e => e.Cart);

            modelBuilder.Entity<CartItems>()
               .HasOne(s => s.Cart)
               .WithMany(s => s.CartItems);

        }
    }
}
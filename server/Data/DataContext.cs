using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class DataContext : DbContext
    {
        // Sets up a database connection
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Creates a "Users" table in the database
        public DbSet<UserModel> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Makes sure that the email and idOrPassport are unique
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email)  
                .IsUnique();

            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.IdOrPassport)  
                .IsUnique();
        }
    }
}

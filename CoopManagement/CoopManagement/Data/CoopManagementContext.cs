using CoopManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoopManagementApp.Data
{
    public class CoopManagementContext : DbContext
    {
        public CoopManagementContext(DbContextOptions<CoopManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map the Transaction model to the "Transaction" table in the database
            modelBuilder.Entity<Transaction>().ToTable("Transaction");  // Change this if your table name is different

            base.OnModelCreating(modelBuilder);
        }
    }
}
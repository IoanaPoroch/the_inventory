using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace DomainLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products
        {
            get;
            set;
        }
        public DbSet<Warehouse> Warehouses
        {
            get;
            set;
        }
        public DbSet<Supplier> Suppliers
        {
            get;
            set;
        }
        public DbSet<StockLevels> StockLevels
        {
            get;
            set;
        }

        public DbSet<InventoryMovements> InventoryMovements
        {
            get;
            set;
        }
    }
}
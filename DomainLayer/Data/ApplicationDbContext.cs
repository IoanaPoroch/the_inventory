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

            builder.Ignore<System.Globalization.StringInfo>();

            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<StockLevels>()
                   .HasOne(s => s.Warehouse)
                   .WithMany()
                   .HasForeignKey(s => s.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
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
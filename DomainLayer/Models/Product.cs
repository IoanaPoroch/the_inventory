using System.Globalization;

namespace DomainLayer.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public StringInfo MadeIn { get; set; }
        public decimal Price { get; set; }
        public int WarehouseId { get; set; }
        public int? SupplierId { get; set; }

        public Warehouse Warehouse { get; set; }
        public Supplier Supplier { get; set; }

    }
}

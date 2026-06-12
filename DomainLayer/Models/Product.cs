namespace DomainLayer.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string MadeIn { get; set; }
        public decimal Price { get; set; }
        public int WarehouseId { get; set; }
        public int? SupplierId { get; set; }

        public Warehouse Warehouse { get; set; }
        public Supplier Supplier { get; set; }

    }
}

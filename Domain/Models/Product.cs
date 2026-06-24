using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public string? Color { get; set; }
        [MaxLength(100)]
        public required string MadeIn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

    }
}

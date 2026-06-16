using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Color { get; set; }
        public required string MadeIn { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

    }
}

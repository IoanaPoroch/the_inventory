using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Supplier : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public required string Address { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

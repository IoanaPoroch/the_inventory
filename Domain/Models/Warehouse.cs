namespace Domain.Models
{
    public class Warehouse : BaseEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

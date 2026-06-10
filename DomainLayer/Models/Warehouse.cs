namespace DomainLayer.Models
{
    public class Warehouse : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}

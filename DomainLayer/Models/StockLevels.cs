namespace DomainLayer.Models
{
    public class StockLevels : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehuseId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}

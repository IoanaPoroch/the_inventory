namespace Presentation.DTOs.Requests
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string MadeIn { get; set; }
        public decimal Price { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid SupplierId { get; set; }

    }
}

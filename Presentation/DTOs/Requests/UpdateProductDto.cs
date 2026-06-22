namespace Presentation.DTOs.Requests
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string MadeIn { get; set; }
        public decimal Price { get; set; }
        public Guid WarehouseId { get; set; }
    }
}

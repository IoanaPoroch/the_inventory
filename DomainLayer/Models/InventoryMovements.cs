using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class InventoryMovements : BaseEntity
    {
        public int Id { get; set; }
        public MovementType Type { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int WarehouseId { get; set; }

    }
}

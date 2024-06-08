namespace ELibrary.Orders.Application.Dtos
{
    public class OrderItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

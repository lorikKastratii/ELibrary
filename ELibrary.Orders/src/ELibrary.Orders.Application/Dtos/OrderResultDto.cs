namespace ELibrary.Orders.Application.Dtos
{
    public class OrderResultDto
    {
        public OrderDto Order { get; set; }
        public List<OrderItemDto> OutOfStockItems { get; set; }
    }
}

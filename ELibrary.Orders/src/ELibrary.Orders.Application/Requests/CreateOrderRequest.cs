using ELibrary.Orders.Application.Dtos;

namespace ELibrary.Orders.Application.Requests
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingCountry { get; set; }
    }
}

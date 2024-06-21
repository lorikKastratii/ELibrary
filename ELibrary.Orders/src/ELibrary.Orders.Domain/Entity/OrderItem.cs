namespace ELibrary.Orders.Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
    }
}

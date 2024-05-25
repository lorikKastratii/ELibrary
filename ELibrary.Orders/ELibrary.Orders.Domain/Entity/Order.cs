namespace ELibrary.Orders.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

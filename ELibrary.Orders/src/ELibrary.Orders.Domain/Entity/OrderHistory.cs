namespace ELibrary.Orders.Domain.Entity
{
    public class OrderHistory : BaseEntity
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public Order Order { get; set; }
    }
}

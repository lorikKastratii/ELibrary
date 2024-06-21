namespace ELibrary.Orders.Domain.Entity
{
    public class Shipment : BaseEntity
    {
        public int OrderId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }

        public Order Order { get; set; }
    }
}

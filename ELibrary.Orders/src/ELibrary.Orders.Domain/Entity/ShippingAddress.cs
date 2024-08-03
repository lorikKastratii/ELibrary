﻿namespace ELibrary.Orders.Domain.Entity
{
    public class ShippingAddress : BaseEntity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

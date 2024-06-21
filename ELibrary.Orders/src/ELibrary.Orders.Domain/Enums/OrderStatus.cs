using Microsoft.AspNetCore.Http.HttpResults;

namespace ELibrary.Orders.Domain.Enums
{
    public enum OrderStatus
    {
        Created = 1,
        Delivered = 2,
        InProgress = 3,
        Completed = 4,
        Failed = 5
    }
}

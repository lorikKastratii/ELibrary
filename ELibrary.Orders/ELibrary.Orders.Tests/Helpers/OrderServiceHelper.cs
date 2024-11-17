using Bogus;
using ELibrary.Orders.Application.Models;
using ELibrary.Orders.Application.Requests;
using ELibrary.Orders.Domain.Entity;
using OrderItemDto = ELibrary.Orders.Application.Dtos.OrderItemDto;

namespace ELibrary.Orders.Tests.Helpers
{
    public static class OrderServiceHelper
    {               
        public static CreateOrderRequest GenerateOrderRequest(int userId)
        {
            return new Faker<CreateOrderRequest>()
                .RuleFor(x => x.OrderItems, GenerateOrderItemDtoList())
                .RuleFor(x => x.UserId, userId)
                .RuleFor(x => x.ShippingAddress, f => f.Person.Address.Street)
                .RuleFor(x => x.ShippingCity, f => f.Person.Address.City)
                .RuleFor(x => x.ShippingCountry, f => f.Person.Address.State)
                .RuleFor(x => x.ShippingPostalCode, f => f.Person.Address.ZipCode)
                .RuleFor(x => x.ShippingState, f => f.Person.Address.State)
                .Generate();
        }

        public static List<OrderItemDto> GenerateOrderItemDtoList()
        {
            return new Faker<OrderItemDto>()
                .RuleFor(x => x.BookId, f => f.Random.Int(1, 10))
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(x => x.UnitPrice, f => f.Random.Decimal(1, 10))
                .Generate(2);
        }

        public static User GenerateUser()
        {
            return new Faker<User>()
                .RuleFor(x => x.Username, f => f.Person.UserName)
                .RuleFor(x => x.Username, f => f.Person.Email)
                .Generate();
        }

        public static Book GenerateBook(int stock)
        {
            return new Faker<Book>()
                .RuleFor(x => x.ISBN, f => f.Random.String())
                .RuleFor(x => x.Id, f => f.Random.Int(1, 10))
                .RuleFor(x => x.StockQuantity, stock)
                .Generate();
        }
    }
}

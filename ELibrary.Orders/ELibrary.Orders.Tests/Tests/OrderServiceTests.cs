using ELibrary.Orders.Application.Common.Errors;
using ELibrary.Orders.Tests.Builders;
using ELibrary.Orders.Tests.Helpers;
using FluentAssertions;

namespace ELibrary.Orders.Tests.Tests
{
    public class OrderServiceTests
    {
        private const int UserId = 123;

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnSuccess_WithValidData()
        {
            //Arrange
            var request = OrderServiceHelper.GenerateOrderRequest(UserId);
            var user = OrderServiceHelper.GenerateUser();
            var book = OrderServiceHelper.GenerateBook(stock: 10);

            var builder = new OrderServiceBuilder()
                .WithOrderRepository()
                .WithGetUserById(UserId, user)
                .WithGetBook(book);

            var sut = builder.Build();

            //Act
            var result = await sut.CreateOrderAsync(request);

            //Assert
            result.IsSuccess.Should().Be(true);
            result.Data.Should().NotBeNull();

            builder
                .VerifyGetBookIsCalled((byte)request.OrderItems.Count)
                .VerifyGetUserByIdIsCalled()
                .VerifyOrderRepositoryIsCalled()
                .VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task CreateOrderAsync_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            var request = OrderServiceHelper.GenerateOrderRequest(UserId);

            var builder = new OrderServiceBuilder()
                .WithOrderRepository()
                .WithGetUserById(UserId, null);

            var sut = builder.Build();

            //Act
            var result = await sut.CreateOrderAsync(request);

            //Assert
            result.IsSuccess.Should().Be(false);
            result.Data.Should().BeNull();
            result.Error?.Equals(OrderErrors.UserNotFound);

            builder
                .VerifyGetUserByIdIsCalled()
                .VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task CreateOrderAsync_ShouldReturnFailure_WhenStockIsNotAvailable()
        {
            //Arrange
            var request = OrderServiceHelper.GenerateOrderRequest(UserId);
            var user = OrderServiceHelper.GenerateUser();
            var book = OrderServiceHelper.GenerateBook(stock: 0);

            var builder = new OrderServiceBuilder()
                .WithOrderRepository()
                .WithGetBook(book)
                .WithGetUserById(UserId, user);

            var sut = builder.Build();

            //Act
            var result = await sut.CreateOrderAsync(request);

            //Assert
            result.IsSuccess.Should().Be(false);
            result.Data.Should().BeNull();
            result.Error?.Equals(OrderErrors.StockEmpty);

            builder
                .VerifyGetUserByIdIsCalled()
                .VerifyGetBookIsCalled((byte)request.OrderItems.Count)
                .VerifyNoOtherCalls();
        }
    }
}

using AutoMapper;
using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Application.Models;
using ELibrary.Orders.Application.Services;
using ELibrary.Orders.Domain.Entity;
using ELibrary.Orders.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace ELibrary.Orders.Tests.Builders
{
    public class OrderServiceBuilder
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerServiceMock;
        private readonly Mock<IBookClient> _bookClientMock;
        private readonly Mock<IUserClient> _userClientMock;
        private readonly Mock<IMapper> _mapperMock;

        public OrderServiceBuilder()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _loggerServiceMock = new Mock<ILogger<OrderService>>();
            _bookClientMock = new Mock<IBookClient>();
            _userClientMock = new Mock<IUserClient>();
            _mapperMock = new Mock<IMapper>();
        }

        public OrderServiceBuilder WithOrderRepository(bool shouldFail = false)
        {
            _orderRepositoryMock.Setup(x => x.CreateOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(shouldFail ? false : true);

            return this;
        }

        public OrderServiceBuilder WithGetUserById(int userId, User user)
        {
            _userClientMock.Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            return this;
        }
        
        public OrderServiceBuilder WithGetBook(Book book)
        {
            _bookClientMock.Setup(x => x.GetBookAsync(It.IsAny<int>()))
                .ReturnsAsync(book);

            return this;
        }

        public OrderService Build()
        {
            return new OrderService(_orderRepositoryMock.Object, _bookClientMock.Object,
                _mapperMock.Object, _loggerServiceMock.Object, _userClientMock.Object);
        }
    }
}

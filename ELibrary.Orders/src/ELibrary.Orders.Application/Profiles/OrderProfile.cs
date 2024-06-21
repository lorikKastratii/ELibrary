using AutoMapper;
using ELibrary.Orders.Application.Dtos;
using ELibrary.Orders.Application.Requests;

namespace ELibrary.Orders.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, OrderDto>();
        }
    }
}

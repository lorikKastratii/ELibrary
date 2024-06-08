using AutoMapper;
using ELibrary.Orders.Application.Clients.Interfaces;
using ELibrary.Orders.Application.Interfaces;
using ELibrary.Orders.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Orders.PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();

            if (orders.Any())
            {
                return Ok(orders);
            }

            return NotFound();
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
        {
            if(request is null)
            {
                return BadRequest();
            }

            var response = await _orderService.CreateOrderAsync(request);

            if(response is null)
            {
                return Ok("Failed to create order");
            }

            return Ok(response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Services.Application.Services;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Contracts.Responses;

namespace PizzaPlace.Services
{
    [ApiController]
    [Route("api/v1/{controller}")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<OrderFound>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken = default)
        {
            var getOrderResult = await _orderService.GetOrderAsync(id, cancellationToken);
            return getOrderResult.Match<IActionResult>(
                order => new OkObjectResult(OrderFound.FromOrder(order)),
                _ => NotFound());
        }

        [HttpPost]
        [ProducesResponseType<OrderCreated>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder order, CancellationToken cancellationToken = default)
        {
            var createOrderResult = await _orderService.CreateOrderAsync(order, cancellationToken);
            return createOrderResult.Match<IActionResult>(
                orderCreated => new OkObjectResult(OrderCreated.FromOrder(orderCreated)),
                _ => BadRequest());
        }
    }
}
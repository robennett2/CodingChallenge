using System.Runtime.CompilerServices;
using FluentValidation;
using PizzaPlace.Services.Application.Infrastructure;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Domain.Entities;
using PizzaPlace.Services.Infrastructure.Exceptions;

namespace PizzaPlace.Services.Application.Services;

public class OrderService : IOrderService
{
    private readonly IValidator<CreateOrder> _createOrderValidator;
    private readonly IItemRepository _itemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderPriceCalculatorService _orderPriceCalculatorService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IValidator<CreateOrder> createOrderValidator,
        IItemRepository itemRepository, 
        IOrderRepository orderRepository, 
        IOrderPriceCalculatorService orderPriceCalculatorService,
        ILogger<OrderService> logger)
    {
        _createOrderValidator = createOrderValidator;
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
        _orderPriceCalculatorService = orderPriceCalculatorService;
        _logger = logger;
    }

    public async Task<CreateOrderResult> CreateOrderAsync(CreateOrder request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating order for {CustomerName}", request.CustomerName);

        if ((await _createOrderValidator.ValidateAsync(request, cancellationToken)).IsValid == false)
        {
            return new CreateOrderResult.BadRequest();
        }

        try
        {
            var orderLines = await CreateOrderLinesAsync(request.OrderLines, cancellationToken).ToListAsync(cancellationToken);
            
            var order = new Order(
                request.CustomerName,
                orderLines,
                await _orderPriceCalculatorService.CalculatePriceAsync(orderLines, cancellationToken));

            await _orderRepository.CreateOrderAsync(order, cancellationToken);
            return order;
        }
        catch (ItemNotFoundException e)
        {
            return new CreateOrderResult.BadRequest();
        }
    }

    public async Task<GetOrderResult> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _orderRepository.GetOrderAsync(id, cancellationToken);
        }
        catch (OrderNotFoundException)
        {
            return new GetOrderResult.OrderNotFound();
        }
    }

    private async IAsyncEnumerable<OrderLine> CreateOrderLinesAsync(
        IReadOnlyCollection<CreateOrder.OrderLine> orderLines, 
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var orderLine in orderLines)
        {
            yield return new OrderLine(await _itemRepository.GetItemAsync(orderLine.ItemId, cancellationToken), orderLine.Quantity);
        }
    }
}
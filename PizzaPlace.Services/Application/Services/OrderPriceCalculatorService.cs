using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Application.Services;

public class OrderPriceCalculatorService : IOrderPriceCalculatorService
{
    public async Task<decimal> CalculatePriceAsync(IReadOnlyCollection<OrderLine> orderLines, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return orderLines.Sum(orderLine => orderLine.Item.Price * orderLine.Quantity);
    }
}
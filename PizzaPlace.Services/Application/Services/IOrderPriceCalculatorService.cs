using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Application.Services;

public interface IOrderPriceCalculatorService
{
    Task<decimal> CalculatePriceAsync(IReadOnlyCollection<OrderLine> orderLines, CancellationToken cancellationToken = default);
}
using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Application.Infrastructure;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
}

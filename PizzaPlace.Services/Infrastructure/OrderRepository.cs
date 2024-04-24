using PizzaPlace.Services.Application.Infrastructure;
using PizzaPlace.Services.Domain.Entities;
using PizzaPlace.Services.Infrastructure.Exceptions;

namespace PizzaPlace.Services.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly IDataStore _dataStore;

    public OrderRepository(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return _dataStore.Orders.SingleOrDefault(i => i.Id == id) ?? throw new OrderNotFoundException(id);
    }

    public async Task CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        _dataStore.AddOrder(order);
    }
}
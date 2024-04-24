using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Application.Infrastructure;

public interface IItemRepository
{
    Task<Item> GetItemAsync(int id, CancellationToken cancellationToken = default);
}
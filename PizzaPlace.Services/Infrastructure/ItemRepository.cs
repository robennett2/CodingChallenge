using PizzaPlace.Services.Application.Infrastructure;
using PizzaPlace.Services.Domain.Entities;
using PizzaPlace.Services.Infrastructure.Exceptions;

namespace PizzaPlace.Services.Infrastructure;

public class ItemRepository : IItemRepository
{
    private readonly IDataStore _dataStore;

    public ItemRepository(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<Item> GetItemAsync(int id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return _dataStore.Items.SingleOrDefault(i => i.Id == id) ?? throw new ItemNotFoundException(id);
    }
}
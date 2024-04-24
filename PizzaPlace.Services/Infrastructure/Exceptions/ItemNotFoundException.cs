namespace PizzaPlace.Services.Infrastructure.Exceptions;

public class ItemNotFoundException : Exception
{
    public int ItemId { get; }

    public ItemNotFoundException(int itemId) : base($"Item with id {itemId} not found")
    {
        ItemId = itemId;
    }
}
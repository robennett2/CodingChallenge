namespace PizzaPlace.Services.Domain.Entities;

public class Item
{
    public Item(int itemId, string itemName, decimal price)
    {
        ItemId = itemId;
        ItemName = itemName;
        Price = price;
    }
    
    public int ItemId { get; private set; }
    
    public string ItemName { get; private set; }

    public decimal Price { get; }
}
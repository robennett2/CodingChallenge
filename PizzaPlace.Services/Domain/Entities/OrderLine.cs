namespace PizzaPlace.Services.Domain.Entities;

public class OrderLine
{
    public OrderLine(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
    
    public Item Item { get; private set; }
    
    public int Quantity { get; private set; }
}
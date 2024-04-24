namespace PizzaPlace.Services.Domain.Entities;

public class Item
{
    public Item(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
    
    public int Id { get; private set; }
    
    public string Name { get; private set; }

    public decimal Price { get; }
}
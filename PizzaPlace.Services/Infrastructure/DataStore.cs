using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Infrastructure;

public interface IDataStore
{
    IReadOnlyCollection<Item> Items { get; }

    IReadOnlyCollection<Order> Orders { get; }
    
    void AddOrder(Order order);
}

public class DataStore : IDataStore
{
    private readonly List<Order> _orders;
    
    public IReadOnlyCollection<Item> Items { get; private set; } = new List<Item>
    {
        new (1, "Veggie Pizza", 12.50m),
        new (2, "Pepperoni Pizza", 14.50m),
        new (3, "Chicken Pizza", 15.00m),
        new (4, "Soda", 2.50m),
        new (5, "Caesar Salad", 7.50m),
        new (6, "Garlic Bread", 4.50m)
    };

    public IReadOnlyCollection<Order> Orders => _orders;

    public DataStore()
    {
        _orders =
        [
            new(
            Guid.NewGuid(),
        "John Doe",
        new List<OrderLine>
            {
                new OrderLine(Items.Single(x => x.Id == 1), 1),
                new OrderLine(Items.Single(x => x.Id == 2), 2),
            },
        41.50m),
        new(
            Guid.NewGuid(),
            "Jane Doe",
            new List<OrderLine>
            {
                new OrderLine(Items.Single(x => x.Id == 3), 1),
                new OrderLine(Items.Single(x => x.Id == 4), 2),
            },
            41.50m)
        ];
    }
    
    public void AddOrder(Order order) => _orders.Add(order);
}
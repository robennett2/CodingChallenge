using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Infrastructure;

public static class DataStore
{
    public static IReadOnlyCollection<Item> Items { get; private set; } = new List<Item>
    {
        new (1, "Veggie Pizza", 12.50m),
        new (2, "Pepperoni Pizza", 14.50m),
        new (3, "Chicken Pizza", 15.00m),
        new (4, "Soda", 2.50m),
        new (5, "Caesar Salad", 7.50m),
        new (6, "Garlic Bread", 4.50m)
    };

    private static readonly List<Order> _orders =
    [
        new(
            1,
            "John Doe",
            new List<OrderLine>
            {
                new OrderLine(Items.Single(x => x.ItemId == 1), 1),
                new OrderLine(Items.Single(x => x.ItemId == 2), 2),
            },
            41.50m),

        new(
            2,
            "Jane Doe",
            new List<OrderLine>
            {
                new OrderLine(Items.Single(x => x.ItemId == 3), 1),
                new OrderLine(Items.Single(x => x.ItemId == 4), 2),
            },
            41.50m)
    ];

    public static void AddOrder(Order order) => _orders.Add(order);

    public static IReadOnlyCollection<Order> Orders => _orders;
}
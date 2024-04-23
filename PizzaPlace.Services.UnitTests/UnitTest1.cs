
using PizzaPlace.Services;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var Items = new List<Item>
        {
            new Item { ItemName = "Cheese Pizza", Price = 9.99m, Quantity = 1 },
            new Item { ItemName = "Pepperoni Pizza", Price = 11.99m, Quantity = 1 },
            new Item { ItemName = "Soda", Price = 1.99m, Quantity = 2 }
        };

        
        
        
          // Act
          var result = OrdersController.CalculateTotal(Items);

        // Assert
        Assert.Equal(25.96m, result);
    }
}
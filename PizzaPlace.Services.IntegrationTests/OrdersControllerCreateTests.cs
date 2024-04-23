using System.Net;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests;

public class OrdersControllerCreateTests
{
    private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5151") };


    [Fact]
    public async Task Create_Order_ReturnsSuccess1()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "/create")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new Order
            {
                OrderId = 3,
                CustomerName = "John Smith",
                Items = new List<Item>
                {
                    new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 10.00m, Quantity = 1},
                    new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m, Quantity = 1},
                }
            }), Encoding.UTF8, "application/json")
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_Order_ReturnsSuccess2()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "/create")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new Order
            {
                OrderId = 3,
                CustomerName = "",
                Items = new List<Item>
                {
                    new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 10.00m, Quantity = 1},
                    new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m, Quantity = 1},
                },
                Total = 28.50m
            }), Encoding.UTF8, "application/json")
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    
}
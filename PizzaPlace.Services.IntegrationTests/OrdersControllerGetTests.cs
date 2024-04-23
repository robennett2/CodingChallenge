using System.Net;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests;

public class OrdersControllerTests
{
    private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5151") };

  
    [Fact]
    public async Task Get_Order_ReturnsSuccessStatusCode()
    {
        // Arrange
        var request = "/get?id=1";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    
}
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests;

public class OrdersControllerTests : IntegrationTestBase
{
    [Fact]
    public async Task Get_Order_ReturnsSuccessStatusCode()
    {
        // Arrange
        var request = "/get?id=1";

        // Act
        var response = await Client.GetAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    
}
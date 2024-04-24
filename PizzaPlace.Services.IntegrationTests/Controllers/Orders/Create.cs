using System.Net;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests.Controllers.Orders.Create;

public class Given_I_have_an_order_without_a_total_And_it_is_valid_When_I_make_the_request_with_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private Order _order = null!;

    public Given_I_have_an_order_without_a_total_And_it_is_valid_When_I_make_the_request_with_that_order()
    {
        Given(() =>
        {
            _order = new Order
            {
                OrderId = 3,
                CustomerName = "John Smith",
                Items = new List<Item>
                {
                    new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 10.00m, Quantity = 1 },
                    new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m, Quantity = 1 },
                }
            };
        });
        
        When(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/create")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_order), Encoding.UTF8, "application/json")
            };
            
            return await Client.SendAsync(request);
        });
    }
    
    [Fact]
    public void Then_an_OK_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

public class Given_I_have_an_order_with_a_total_And_it_is_valid_When_I_make_the_request_with_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private Order _order = null!;

    public Given_I_have_an_order_with_a_total_And_it_is_valid_When_I_make_the_request_with_that_order()
    {
        Given(() =>
        {
            _order = new Order
            {
                OrderId = 3,
                CustomerName = "",
                Items = new List<Item>
                {
                    new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 10.00m, Quantity = 1 },
                    new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m, Quantity = 1 },
                },
                Total = 28.50m
            };
        });
        
        When(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/create")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(_order), Encoding.UTF8, "application/json")
            };
            
            return await Client.SendAsync(request);
        });
    }
    
    [Fact]
    public void Then_an_OK_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
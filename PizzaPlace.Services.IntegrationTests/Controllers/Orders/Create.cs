using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Contracts.Responses;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests.Controllers.Orders.Create;

public class Given_I_have_an_order_When_I_make_the_request_to_create_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private CreateOrder _createOrderRequest = null!;

    public Given_I_have_an_order_When_I_make_the_request_to_create_that_order()
    {
        Given(() =>
        {
            _createOrderRequest = new(
                "Customer", 
                new List<CreateOrder.OrderLine>
                {
                    new(1, 2),
                    new(2, 3),
                });
        });
        
        When(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/Orders")
            {
                Content = JsonContent.Create(_createOrderRequest)
            };
            
            return await Client.SendAsync(request);
        });
    }
    
    [Fact]
    public void Then_an_OK_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Then_the_created_order_is_returned()
    {
        var body = await Result.Content.ReadFromJsonAsync<OrderCreated>();
        body.Should().BeEquivalentTo(new
        {
            OrderId = new { },
            OrderLines = new[]
            {
                new
                {
                    ItemId = 1,
                    ItemName = "Veggie Pizza",
                    Quantity = 2
                },
                new
                {
                    ItemId = 2,
                    ItemName = "Pepperoni Pizza",
                    Quantity = 3
                }
            },
            OrderTotal = (2 * 12.50m) + (3 * 14.50m)
        });
    }
}

public class Given_I_have_an_order_that_references_an_item_that_does_not_exist_When_I_make_the_request_to_create_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private CreateOrder _createOrderRequest = null!;

    public Given_I_have_an_order_that_references_an_item_that_does_not_exist_When_I_make_the_request_to_create_that_order()
    {
        Given(() =>
        {
            _createOrderRequest = new(
                "Customer", 
                new List<CreateOrder.OrderLine>
                {
                    new(1, 2),
                    new(-12, 3),
                });
        });
        
        When(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/Orders")
            {
                Content = JsonContent.Create(_createOrderRequest)
            };
            
            return await Client.SendAsync(request);
        });
    }
    
    [Fact]
    public void Then_an_BadRequest_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}


public class Given_I_have_an_order_that_is_invalid_When_I_make_the_request_to_create_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private CreateOrder _createOrderRequest = null!;

    public Given_I_have_an_order_that_is_invalid_When_I_make_the_request_to_create_that_order()
    {
        Given(() =>
        {
            _createOrderRequest = new(
                "Customer", 
                new List<CreateOrder.OrderLine>
                {
                    new(1, 2),
                    new(2, -3),
                });
        });
        
        When(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/Orders")
            {
                Content = JsonContent.Create(_createOrderRequest)
            };
            
            return await Client.SendAsync(request);
        });
    }
    
    [Fact]
    public void Then_an_BadRequest_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
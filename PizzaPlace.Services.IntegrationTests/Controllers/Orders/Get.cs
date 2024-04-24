using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Contracts.Responses;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests.Controllers.Orders.Get;

public class Given_an_order_has_been_created_When_I_make_a_request_to_get_that_order_by_its_id : IntegrationTestBase<HttpResponseMessage>
{
    private Guid _orderId;

    public Given_an_order_has_been_created_When_I_make_a_request_to_get_that_order_by_its_id()
    {
        Given(async () =>
        {
            CreateOrder createOrderRequest = new(
                "Customer", 
                new List<CreateOrder.OrderLine>
                {
                    new(1, 2),
                    new(2, 3),
                });
            
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/Orders")
            {
                Content = JsonContent.Create(createOrderRequest)
            };
            
            var response = await Client.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<OrderCreated>();
            _orderId = result?.OrderId ?? throw new Exception("Given step failed");
        });
        
        When(async () => await Client.GetAsync($"/api/v1/Orders/{_orderId}"));
    }
    
    [Fact]
    public void Then_an_OK_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

public class Given_there_is_not_an_order_with_the_given_id_When_I_make_a_request_to_get_an_order_with_that_id : IntegrationTestBase<HttpResponseMessage>
{
    private Guid _orderId;

    public Given_there_is_not_an_order_with_the_given_id_When_I_make_a_request_to_get_an_order_with_that_id()
    {
        Given(() =>
        {
            _orderId = Guid.NewGuid();
        });
        
        When(async () => await Client.GetAsync($"/api/v1/Orders/{_orderId}"));
    }
    
    [Fact]
    public void Then_an_NotFound_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
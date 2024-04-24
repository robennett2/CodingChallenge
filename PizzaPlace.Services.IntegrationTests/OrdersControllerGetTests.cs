using System.Net;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace PizzaPlace.Services.IntegrationTests.OrdersControllerGetTests;

public class Given_an_order_with_an_ID_exists_When_I_make_a_request_to_get_that_order : IntegrationTestBase<HttpResponseMessage>
{
    private int _orderId = 1;

    public Given_an_order_with_an_ID_exists_When_I_make_a_request_to_get_that_order()
    {
        Given(() =>
        {
            _orderId = 1;
        });
        
        When(async () => await Client.GetAsync($"/get?id={_orderId}"));
    }
    
    [Fact]
    public void Then_an_OK_status_code_returned()
    {
        Result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
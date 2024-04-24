using Chill;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PizzaPlace.Services.IntegrationTests;

public abstract class IntegrationTestBase<TResult> : GivenWhenThen<TResult>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory = new();
    
    protected HttpClient Client { get; private set; }

    protected IntegrationTestBase()
    {
        Client = _webApplicationFactory.CreateClient();
    }
}
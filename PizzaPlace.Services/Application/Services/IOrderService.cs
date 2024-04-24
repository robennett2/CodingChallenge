using OneOf;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Application.Services;

[GenerateOneOf]
public partial class CreateOrderResult : OneOfBase<Order, CreateOrderResult.BadRequest>
{
    public record BadRequest();
}

[GenerateOneOf]
public partial class GetOrderResult : OneOfBase<Order, GetOrderResult.OrderNotFound>
{
    public record OrderNotFound();
}


public interface IOrderService
{
    Task<CreateOrderResult> CreateOrderAsync(CreateOrder order, CancellationToken cancellationToken = default);

    Task<GetOrderResult> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
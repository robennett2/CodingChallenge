using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Contracts.Responses;

public record OrderCreated(
    Guid OrderId,
    IReadOnlyCollection<OrderCreated.OrderLine> OrderLines,
    decimal OrderTotal)
{
    public record OrderLine(int ItemId, string ItemName, int Quantity);
    
    public static OrderCreated FromOrder(Order order) => order;
    
    public static implicit operator OrderCreated(Domain.Entities.Order order)
    {
        return new OrderCreated(
            order.Id,
            order.OrderLines.Select(orderLine => new OrderLine(orderLine.Item.Id, orderLine.Item.Name, orderLine.Quantity)).ToList(),
            order.Total
        );
    }
}
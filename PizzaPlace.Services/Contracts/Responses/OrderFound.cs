using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.Contracts.Responses;

public record OrderFound(
    Guid OrderId,
    IReadOnlyCollection<OrderFound.OrderLine> OrderLines,
    decimal OrderTotal)
{
    public record OrderLine(int ItemId, string ItemName, int Quantity);
    
    public static OrderFound FromOrder(Order order) => order;
    
    public static implicit operator OrderFound(Order order)
    {
        return new OrderFound(
            order.Id,
            order.OrderLines.Select(orderLine => new OrderLine(orderLine.Item.Id, orderLine.Item.Name, orderLine.Quantity)).ToList(),
            order.Total
        );
    }
}
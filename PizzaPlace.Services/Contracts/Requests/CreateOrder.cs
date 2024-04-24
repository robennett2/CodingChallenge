namespace PizzaPlace.Services.Contracts.Requests;

public record CreateOrder(
    string CustomerName,
    IReadOnlyCollection<CreateOrder.OrderLine> OrderLines)
{
    public record OrderLine(int ItemId, int Quantity);
};
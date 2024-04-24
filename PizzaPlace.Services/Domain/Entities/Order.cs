namespace PizzaPlace.Services.Domain.Entities;

public class Order
{
    public Order(int orderId, string customerName, List<OrderLine> orderLines, decimal expectedTotal)
    {
        OrderId = orderId;
        CustomerName = customerName;
        OrderLines = orderLines;
        ExpectedTotal = expectedTotal;
    }

    public int OrderId { get; set; }

    public string CustomerName { get; set; }

    public IReadOnlyCollection<OrderLine> OrderLines { get; set; }
    
    public decimal ExpectedTotal { get; set; }
}
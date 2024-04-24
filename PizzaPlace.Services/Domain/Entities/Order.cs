namespace PizzaPlace.Services.Domain.Entities;

public class Order
{
    public Order(string customerName, List<OrderLine> orderLines, decimal total)
    {
        OrderId = 0;
        CustomerName = customerName;
        OrderLines = orderLines;
        Total = total;
    }
    
    public Order(int orderId, string customerName, List<OrderLine> orderLines, decimal total)
    {
        OrderId = orderId;
        CustomerName = customerName;
        OrderLines = orderLines;
        Total = total;
    }

    public int OrderId { get; set; }

    public string CustomerName { get; set; }

    public IReadOnlyCollection<OrderLine> OrderLines { get; set; }
    
    public decimal Total { get; set; }
}
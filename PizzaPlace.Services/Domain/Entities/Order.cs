namespace PizzaPlace.Services.Domain.Entities;

public class Order
{
    public Order(string customerName, List<OrderLine> orderLines, decimal total)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        OrderLines = orderLines;
        Total = total;
    }
    
    public Order(Guid id, string customerName, List<OrderLine> orderLines, decimal total)
    {
        Id = id;
        CustomerName = customerName;
        OrderLines = orderLines;
        Total = total;
    }

    public Guid Id { get; set; }

    public string CustomerName { get; set; }

    public IReadOnlyCollection<OrderLine> OrderLines { get; set; }
    
    public decimal Total { get; set; }
}
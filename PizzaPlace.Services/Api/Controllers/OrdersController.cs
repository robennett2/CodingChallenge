using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using PizzaPlace.Services.Domain.Entities;
using PizzaPlace.Services.Infrastructure;

namespace PizzaPlace.Services
{
    [ApiController]
    public class OrdersController(ILogger<OrdersController> logger) : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public Order Get(int id)
        {
            return DataStore.Orders.Single(o => o.OrderId == id);
        }


        [HttpPost]
        [Route("create")]
        public Order Create([FromBody] Order order)
        {
            logger.LogInformation("Creating order for {0}", order.CustomerName);
            
            // calculate the total of the order
            order.Total = CalculateTotal(order.OrderLines);
            
            DataStore.AddOrder(order);
            return order;
        }
        
        public static decimal CalculateTotal(IReadOnlyCollection<OrderLine> orderLines)
        {
            decimal total = 0;  
            foreach (var orderLine in orderLines)
            {
                total += orderLine.Item.Price * orderLine.Quantity;
            }
            
            return total;
        }
        
    }
}
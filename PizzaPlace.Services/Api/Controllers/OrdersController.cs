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
            order.Total = CalculateTotal(order.Items);
            
            orders.Add(order);
            return order;
        }
        
        public static decimal CalculateTotal(List<OrderLine> items)
        {
            decimal total = 0;  
            for (int i =0; i < items.Count; i++)
            {
                total += items[i].Price * items[i].Quantity;
            }
            
            return total;
        }
        
    }
}
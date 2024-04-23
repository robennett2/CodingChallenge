using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace PizzaPlace.Services
{
    [ApiController]
    public class OrdersController(ILogger<OrdersController> logger) : ControllerBase
    {
        private List<Item> Menu = new List<Item>
        {
            new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 12.50m },
            new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m },
            new Item { ItemId = 3, ItemName = "Chicken Pizza", Price = 15.00m },
            new Item { ItemId = 4, ItemName = "Soda", Price = 2.50m },
            new Item { ItemId = 5, ItemName = "Caesar Salad", Price = 7.50m },
            new Item { ItemId = 6, ItemName = "Garlic Bread", Price = 4.50m }
        };


        // We have no database, so we'll use a list to store orders
        // Initialize with some sample data
        List<Order> orders = new List<Order>
        {
            new Order
            {
                OrderId = 1, CustomerName = "John Doe", Items = new List<Item>
                {
                    new Item { ItemId = 1, ItemName = "Veggie Pizza", Price = 12.50m, Quantity = 1},
                    new Item { ItemId = 2, ItemName = "Pepperoni Pizza", Price = 14.50m, Quantity = 2},
                },
                Total = 41.50m
            },
            new Order
            {
                OrderId = 2, CustomerName = "Jane Doe", Items = new List<Item>
                {
                    new Item { ItemId = 3, ItemName = "Chicken Pizza", Price = 15.00m },
                    new Item { ItemId = 4, ItemName = "Soda", Price = 2.50m },
                },
                Total = 17.50m
            }
        };

        [HttpGet]
        [Route("get")]
        public Order Get(int id)
        {
            return orders.Single(o => o.OrderId == id);
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
        
        public static decimal CalculateTotal(List<Item> items)
        {
            decimal total = 0;  
            for (int i =0; i < items.Count; i++)
            {
                total += items[i].Price * items[i].Quantity;
            }
            
            return total;
        }
        
    }

    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public List<Item> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
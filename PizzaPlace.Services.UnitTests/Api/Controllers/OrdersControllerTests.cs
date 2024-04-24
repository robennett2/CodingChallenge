using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using PizzaPlace.Services.Application.Services;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Contracts.Responses;
using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.UnitTests.Api.Controllers;

public class OrdersControllerTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private Mock<IOrderService> OrderServiceMock => _mocker.GetMock<IOrderService>();
    private readonly OrdersController _sut;

    public OrdersControllerTests()
    {
        _sut = _mocker.CreateInstance<OrdersController>();
    }

    [Fact]
    public async Task CreateOrder_WasSuccessfullyCreated_ReturnsCreatedOrder()
    {
        // Arrange
        var orderLines = new List<OrderLine>()
        {
            new OrderLine(new Item(1, "item 1", 10.00M), 2),
            new OrderLine(new Item(2, "item 2", 12.00M), 3)
        };
        
        var order = new Order(
            Guid.NewGuid(), 
            "customer", 
            orderLines, 
            46.00M);
            
        var request = new CreateOrder(order.CustomerName, new List<CreateOrder.OrderLine>()
        {
            new(orderLines[0].Item.Id, orderLines[0].Quantity),
            new(orderLines[1].Item.Id, orderLines[1].Quantity)
        });
        
        OrderServiceMock
            .Setup(x => x.CreateOrderAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act
        IActionResult result = await _sut.CreateOrder(request);

        // Assert
        var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okObjectResult.Value.Should().BeOfType<OrderCreated>().Subject.Should().BeEquivalentTo(new
        {
            OrderId = order.Id,
            OrderLines = new []
            {
                new
                {
                    ItemId = 1,
                    ItemName = "item 1",
                    Quantity = 2
                },
                new
                {
                    ItemId = 2,
                    ItemName = "item 2",
                    Quantity = 3
                }
            },
            OrderTotal = 46.00M
        });
    }
    
    [Fact]
    public async Task CreateOrder_WasNotCreatedDueToBadRequest_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateOrder("customer name", new List<CreateOrder.OrderLine>()
        {
            new CreateOrder.OrderLine(1, 2),
            new CreateOrder.OrderLine(2, 3)
        });
        
        OrderServiceMock
            .Setup(x => x.CreateOrderAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateOrderResult.BadRequest());

        // Act
        IActionResult result = await _sut.CreateOrder(request);

        // Assert
        var okObjectResult = result.Should().BeOfType<BadRequestResult>().Subject;
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GetOrder_ThatExists_ReturnsCorrectObject()
    {
        // Arrange
        var orderLines = new List<OrderLine>()
        {
            new OrderLine(new Item(1, "item 1", 10.00M), 2),
            new OrderLine(new Item(2, "item 2", 12.00M), 3)
        };
        
        var order = new Order(
            Guid.NewGuid(), 
            "customer", 
            orderLines, 
            46.00M);
        
        OrderServiceMock
            .Setup(x => x.GetOrderAsync(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        
        // Act
        var result = await _sut.GetOrder(order.Id);
        
        // Assert
        var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okObjectResult.Value.Should().BeOfType<OrderFound>().Subject.Should().BeEquivalentTo(new
        {
            OrderId = order.Id,
            OrderLines = new []
            {
                new
                {
                    ItemId = 1,
                    ItemName = "item 1",
                    Quantity = 2
                },
                new
                {
                    ItemId = 2,
                    ItemName = "item 2",
                    Quantity = 3
                }
            },
            OrderTotal = 46.00M
        });
    }
}
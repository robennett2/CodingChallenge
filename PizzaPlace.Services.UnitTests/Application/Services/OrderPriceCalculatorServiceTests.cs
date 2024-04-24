using FluentAssertions;
using Moq;
using Moq.AutoMock;
using PizzaPlace.Services.Application.Services;
using PizzaPlace.Services.Domain.Entities;

namespace PizzaPlace.Services.UnitTests.Application.Services;

public class OrderPriceCalculatorServiceTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly OrderPriceCalculatorService _sut;

    public OrderPriceCalculatorServiceTests()
    {
        _sut = _mocker.CreateInstance<OrderPriceCalculatorService>();
    }
        
    [Fact]
    public async Task CalculatePriceAsync_WithNoOrderLine_ShouldReturnZero()
    {
        // Act
        var price = await _sut.CalculatePriceAsync(new List<OrderLine>());

        // Assert
        price.Should().Be(0.00M);
    }
    
    [Fact]
    public async Task CalculatePriceAsync_WithSingleOrderLine_ShouldReturnCorrectPrice()
    {
        // Arrange
        var orderLines = new List<OrderLine>()
        {
            new(new Item(1, "item 1", 10.00M), 1)
        };
        
        // Act
        var price = await _sut.CalculatePriceAsync(orderLines);

        // Assert
        price.Should().Be(10.00M);
    }
    
    [Fact]
    public async Task CalculatePriceAsync_WithMultipleOrderLines_ShouldReturnCorrectPrice()
    {
        // Arrange
        var orderLines = new List<OrderLine>()
        {
            new(new Item(1, "item 1", 10.00M), 2),
            new(new Item(2, "item 2", 12.00M), 3),
            new(new Item(1, "item 3", 10.00M), 1),
        };
        
        // Act
        var price = await _sut.CalculatePriceAsync(orderLines);

        // Assert
        price.Should().Be(66.00M);
    }
}
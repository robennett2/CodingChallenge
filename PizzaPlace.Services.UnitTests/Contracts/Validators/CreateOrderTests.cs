using FluentAssertions;
using PizzaPlace.Services.Contracts.Requests;
using PizzaPlace.Services.Contracts.Validators;

namespace PizzaPlace.Services.UnitTests.Contracts.Validators;

public class CreateOrderTests
{
    private CreateOrderValidator _sut = new CreateOrderValidator();

    [Fact]
    public async Task ValidateAsync_ValidRequest_ReturnsValid()
    {
        // Arrange
        var createOrderRequest = new CreateOrder(
            "Customer Name", 
            new List<CreateOrder.OrderLine>()
            {
                new(1, 4),
                new(2, 4)
            });
        
        // Act
        var result = await _sut.ValidateAsync(createOrderRequest);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData((string?)null)]
    [InlineData("")]
    [InlineData("       ")]
    public async Task ValidateAsync_InvalidCustomerName_ReturnsInvalid(string? customerName)
    {
        // Arrange
        var createOrderRequest = new CreateOrder(
            customerName!, 
            new List<CreateOrder.OrderLine>()
            {
                new(1, 4),
                new(2, 4)
            });
        
        // Act
        var result = await _sut.ValidateAsync(createOrderRequest);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidateAsync_NoOrderLines_ReturnsInvalid()
    {
        // Arrange
        var createOrderRequest = new CreateOrder(
            "Customer Name", 
            new List<CreateOrder.OrderLine>());
        
        // Act
        var result = await _sut.ValidateAsync(createOrderRequest);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ValidateAsync_OrderLineHasQuantityLessThanOne_ReturnsInvalid(int quantity)
    {
        // Arrange
        var createOrderRequest = new CreateOrder(
            "Customer Name", 
            new List<CreateOrder.OrderLine>()
            {
                new(1, quantity),
                new(2, 4)
            });
        
        // Act
        var result = await _sut.ValidateAsync(createOrderRequest);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}
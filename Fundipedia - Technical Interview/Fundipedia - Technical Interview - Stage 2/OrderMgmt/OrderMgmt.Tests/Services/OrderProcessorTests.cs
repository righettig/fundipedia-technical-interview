using Moq;
using OrderMgmt.Domain;
using OrderMgmt.Domain.Rules.Interfaces;
using OrderMgmt.Domain.Services.Impl;

namespace OrderMgmt.Tests.Services;

public class OrderProcessorTests
{
    private readonly Mock<IOrderRule> _mockRule1;
    private readonly Mock<IOrderRule> _mockRule2;
    private readonly Mock<IOrderRule> _mockRule3;

    private readonly OrderProcessor _orderProcessor;

    private readonly Order _order;

    public OrderProcessorTests()
    {
        _mockRule1 = new Mock<IOrderRule>();
        _mockRule2 = new Mock<IOrderRule>();
        _mockRule3 = new Mock<IOrderRule>();

        var rules = new List<IOrderRule> { _mockRule1.Object, _mockRule2.Object, _mockRule3.Object };
        
        _orderProcessor = new OrderProcessor(rules);
        
        _order = new Order { OrderType = OrderType.Repair };
    }

    [Fact]
    public void DetermineOrderStatus_ShouldReturnDefaultStatus_WhenNoRulesApply()
    {
        // Arrange
        _mockRule1.Setup(r => r.CanBeApplied(_order)).Returns(false);
        _mockRule2.Setup(r => r.CanBeApplied(_order)).Returns(false);
        _mockRule3.Setup(r => r.CanBeApplied(_order)).Returns(false);

        // Act
        var result = _orderProcessor.DetermineOrderStatus(_order);

        // Assert
        Assert.Equal(OrderStatus.Confirmed, result); // Default fallback status
    }

    [Fact]
    public void DetermineOrderStatus_ShouldEvaluateRulesInOrderOfPriority()
    {
        // Arrange
        var expectedStatus = OrderStatus.AuthorisationRequired;

        _mockRule1.Setup(r => r.CanBeApplied(_order)).Returns(true); // First rule matches
        _mockRule1.Setup(r => r.Evaluate(_order)).Returns(expectedStatus);

        _mockRule2.Setup(r => r.CanBeApplied(_order)).Returns(true); // Second rule matches
        _mockRule2.Setup(r => r.Evaluate(_order)).Returns(OrderStatus.Closed);

        // Act
        var result = _orderProcessor.DetermineOrderStatus(_order);

        // Assert
        Assert.Equal(expectedStatus, result);
    }

    [Fact]
    public void DetermineOrderStatus_ShouldEvaluateRulesInOrderOfPriority_2()
    {
        // Arrange
        var expectedStatus = OrderStatus.Closed;

        _mockRule1.Setup(r => r.CanBeApplied(_order)).Returns(false); // First rule does NOT match

        _mockRule2.Setup(r => r.CanBeApplied(_order)).Returns(true); // Second rule matches
        _mockRule2.Setup(r => r.Evaluate(_order)).Returns(expectedStatus);

        // Act
        var result = _orderProcessor.DetermineOrderStatus(_order);

        // Assert
        Assert.Equal(expectedStatus, result);
    }
}

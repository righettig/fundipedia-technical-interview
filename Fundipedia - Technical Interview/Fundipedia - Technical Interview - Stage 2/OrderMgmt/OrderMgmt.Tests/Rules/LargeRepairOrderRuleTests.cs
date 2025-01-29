using OrderMgmt.Domain;
using OrderMgmt.Domain.Rules.Impl;

namespace OrderMgmt.Tests.Rules;

public class LargeRepairOrderRuleTests
{
    private readonly LargeRepairOrderRule _rule;

    public LargeRepairOrderRuleTests()
    {
        _rule = new LargeRepairOrderRule();
    }

    [Fact]
    public void Priority_ShouldBe_3()
    {
        Assert.Equal(3, _rule.Priority);
    }

    [Theory]
    [InlineData(true,  OrderType.Repair, true)]  // Valid case
    [InlineData(false, OrderType.Repair, false)] // Not a large order
    [InlineData(true,  OrderType.Hire,   false)] // Not a repair order
    [InlineData(false, OrderType.Hire,   false)] // Not a large order + Not a repair order
    public void CanBeApplied_ShouldReturnExpectedResult(bool isLargeOrder, OrderType orderType, bool expected)
    {
        // Arrange
        var order = new Order
        {
            IsLargeOrder = isLargeOrder,
            OrderType = orderType
        };

        // Act
        var result = _rule.CanBeApplied(order);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Evaluate_ShouldReturn_AuthorisationRequired()
    {
        var order = new Order
        {
            IsLargeOrder = true,
            OrderType = OrderType.Repair
        };

        Assert.Equal(OrderStatus.AuthorisationRequired, _rule.Evaluate(order));
    }
}

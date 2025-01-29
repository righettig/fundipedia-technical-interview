using OrderMgmt.Domain;
using OrderMgmt.Domain.Rules.Impl;

namespace OrderMgmt.Tests.Rules;

public class LargeRushHireOrderRuleTests
{
    private readonly LargeRushHireOrderRule _rule;

    public LargeRushHireOrderRuleTests()
    {
        _rule = new LargeRushHireOrderRule();
    }

    [Fact]
    public void Priority_ShouldBe_2()
    {
        Assert.Equal(2, _rule.Priority);
    }

    [Theory]
    [InlineData(true,  true,  OrderType.Hire,   true)]  // Valid case
    [InlineData(false, true,  OrderType.Hire,   false)] // Not a large order
    [InlineData(true,  false, OrderType.Hire,   false)] // Not a rush order
    [InlineData(true,  true,  OrderType.Repair, false)] // Not a hire order
    [InlineData(false, false, OrderType.Repair, false)] // None of the conditions met
    public void CanBeApplied_ShouldReturnExpectedResult(bool isLargeOrder, bool isRushOrder, OrderType orderType, bool expected)
    {
        // Arrange
        var order = new Order
        {
            IsLargeOrder = isLargeOrder,
            IsRushOrder = isRushOrder,
            OrderType = orderType
        };

        // Act
        var result = _rule.CanBeApplied(order);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Evaluate_ShouldReturn_Closed()
    {
        var order = new Order
        {
            IsLargeOrder = true,
            IsRushOrder = true,
            OrderType = OrderType.Hire
        };

        Assert.Equal(OrderStatus.Closed, _rule.Evaluate(order));
    }
}

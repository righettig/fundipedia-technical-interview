using OrderMgmt.Domain;
using OrderMgmt.Domain.Rules.Impl;

namespace OrderMgmt.Tests.Rules;

public class RushOrderNewCustomerRuleTests
{
    private readonly RushOrderNewCustomerRule _rule;

    public RushOrderNewCustomerRuleTests()
    {
        _rule = new RushOrderNewCustomerRule();
    }

    [Fact]
    public void Priority_ShouldBe_4()
    {
        Assert.Equal(4, _rule.Priority);
    }

    [Theory]
    [InlineData(true,  true,  true)]  // Valid case
    [InlineData(false, true,  false)] // Not a rush order
    [InlineData(true,  false, false)] // Not a new customer
    [InlineData(false, false, false)] // None of the conditions met
    public void CanBeApplied_ShouldReturnExpectedResult(bool isRushOrder, bool isNewCustomer, bool expected)
    {
        // Arrange
        var order = new Order
        {
            IsRushOrder = isRushOrder,
            IsNewCustomer = isNewCustomer,

            // hardcoded as it's not being used in the logic
            OrderType = OrderType.Repair,
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
            IsRushOrder = true,
            IsNewCustomer = true,

            // hardcoded as it's not being used in the logic
            OrderType = OrderType.Repair,
        };

        Assert.Equal(OrderStatus.AuthorisationRequired, _rule.Evaluate(order));
    }
}
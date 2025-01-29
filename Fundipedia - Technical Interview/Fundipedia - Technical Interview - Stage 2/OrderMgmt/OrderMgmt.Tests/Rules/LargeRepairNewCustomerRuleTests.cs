using OrderMgmt.Domain;
using OrderMgmt.Domain.Rules.Impl;

namespace OrderMgmt.Tests.Rules;

public class LargeRepairNewCustomerRuleTests
{
    private readonly LargeRepairNewCustomerRule _rule;

    public LargeRepairNewCustomerRuleTests()
    {
        _rule = new LargeRepairNewCustomerRule();
    }

    [Fact]
    public void Priority_ShouldBe_One()
    {
        Assert.Equal(1, _rule.Priority);
    }

    [Theory]
    [InlineData(true,  OrderType.Repair, true,  true)]  // Valid case
    [InlineData(false, OrderType.Repair, true,  false)] // Not a large order
    [InlineData(true,  OrderType.Hire,   true,  false)] // Not a repair order
    [InlineData(true,  OrderType.Repair, false, false)] // Not a new customer
    public void CanBeApplied_ShouldReturn_CorrectResult(bool isLargeOrder, OrderType orderType, bool isNewCustomer, bool expectedResult)
    {
        var order = new Order 
        { 
            IsLargeOrder = isLargeOrder, 
            OrderType = orderType, 
            IsNewCustomer = isNewCustomer 
        };

        bool result = _rule.CanBeApplied(order);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Evaluate_ShouldReturn_Closed()
    {
        var order = new Order { OrderType = OrderType.Repair };

        OrderStatus result = _rule.Evaluate(order);

        Assert.Equal(OrderStatus.Closed, result);
    }
}

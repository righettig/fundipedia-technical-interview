using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRushHireOrderRule : IOrderRule
{
    public int Priority => 2;

    public bool CanBeApplied(Order order)
    {
        return
            order.IsLargeOrder &&
            order.IsRushOrder &&
            order.OrderType == OrderType.Hire;
    }

    public OrderStatus Evaluate(Order order)
    {
        return OrderStatus.Closed;
    }
}

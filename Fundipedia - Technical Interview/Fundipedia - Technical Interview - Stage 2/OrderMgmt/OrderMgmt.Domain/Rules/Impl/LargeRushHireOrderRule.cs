using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRushHireOrderRule : IOrderRule
{
    public int Priority => 2;

    public OrderStatus Evaluate(Order order)
    {
        if (order.IsLargeOrder && 
            order.IsRushOrder && 
            order.OrderType == OrderType.Hire)
        {
            return OrderStatus.Closed;
        }
        return OrderStatus.AuthorisationRequired;
    }
}

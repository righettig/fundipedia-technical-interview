using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRushHireOrderRule : IOrderRule
{
    public OrderStatus Evaluate(Order orderRequest)
    {
        if (orderRequest.IsLargeOrder && 
            orderRequest.IsRushOrder && 
            orderRequest.OrderType == OrderType.Hire)
        {
            return OrderStatus.Closed;
        }
        return OrderStatus.AuthorisationRequired;
    }
}

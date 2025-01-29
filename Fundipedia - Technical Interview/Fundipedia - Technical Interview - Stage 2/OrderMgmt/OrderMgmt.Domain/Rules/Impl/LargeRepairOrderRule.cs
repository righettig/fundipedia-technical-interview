using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairOrderRule : IOrderRule
{
    public int Priority => 3;

    public OrderStatus Evaluate(Order order)
    {
        if (order.IsLargeOrder && 
            order.OrderType == OrderType.Repair)
        {
            return OrderStatus.AuthorisationRequired;
        }
        return OrderStatus.Confirmed; // Default fallback if not matching
    }
}
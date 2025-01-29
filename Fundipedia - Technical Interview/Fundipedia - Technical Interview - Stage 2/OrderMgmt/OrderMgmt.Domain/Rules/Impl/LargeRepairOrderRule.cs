using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairOrderRule : IOrderRule
{
    public OrderStatus Evaluate(Order orderRequest)
    {
        if (orderRequest.IsLargeOrder && 
            orderRequest.OrderType == OrderType.Repair)
        {
            return OrderStatus.AuthorisationRequired;
        }
        return OrderStatus.Confirmed; // Default fallback if not matching
    }
}
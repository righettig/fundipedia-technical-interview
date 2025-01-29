using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairNewCustomerRule : IOrderRule
{
    public OrderStatus Evaluate(Order order)
    {
        if (order.IsLargeOrder && 
            order.OrderType == OrderType.Repair && 
            order.IsNewCustomer)
        {
            return OrderStatus.Closed;
        }
        return OrderStatus.AuthorisationRequired; // Default fallback if not matching
    }
}
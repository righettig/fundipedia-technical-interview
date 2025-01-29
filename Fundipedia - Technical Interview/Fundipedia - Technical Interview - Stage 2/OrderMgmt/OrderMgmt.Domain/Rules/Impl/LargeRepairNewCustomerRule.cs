using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairNewCustomerRule : IOrderRule
{
    public OrderStatus Evaluate(Order orderRequest)
    {
        if (orderRequest.IsLargeOrder && 
            orderRequest.OrderType == OrderType.Repair && 
            orderRequest.IsNewCustomer)
        {
            return OrderStatus.Closed;
        }
        return OrderStatus.AuthorisationRequired; // Default fallback if not matching
    }
}
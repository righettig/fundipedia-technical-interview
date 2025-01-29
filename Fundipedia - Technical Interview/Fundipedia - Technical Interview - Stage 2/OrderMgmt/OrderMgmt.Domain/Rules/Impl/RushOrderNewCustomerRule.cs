using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class RushOrderNewCustomerRule : IOrderRule
{
    public OrderStatus Evaluate(Order order)
    {
        if (order.IsRushOrder && order.IsNewCustomer)
        {
            return OrderStatus.AuthorisationRequired;
        }
        return OrderStatus.Confirmed; // Default fallback if not matching
    }
}
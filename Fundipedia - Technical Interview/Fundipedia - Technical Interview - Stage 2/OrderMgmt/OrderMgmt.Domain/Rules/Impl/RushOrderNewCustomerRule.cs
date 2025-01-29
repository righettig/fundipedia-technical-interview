using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class RushOrderNewCustomerRule : IOrderRule
{
    public OrderStatus Evaluate(Order orderRequest)
    {
        if (orderRequest.IsRushOrder && orderRequest.IsNewCustomer)
        {
            return OrderStatus.AuthorisationRequired;
        }
        return OrderStatus.Confirmed; // Default fallback if not matching
    }
}
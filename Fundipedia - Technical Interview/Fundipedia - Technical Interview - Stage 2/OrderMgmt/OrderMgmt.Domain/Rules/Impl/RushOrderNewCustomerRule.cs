using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class RushOrderNewCustomerRule : IOrderRule
{
    public int Priority => 4;

    public bool CanBeApplied(Order order)
    {
        return order.IsRushOrder && order.IsNewCustomer;
    }

    public OrderStatus Evaluate(Order order)
    {
        return OrderStatus.AuthorisationRequired;
    }
}
using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairNewCustomerRule : IOrderRule
{
    public int Priority => 1;

    public bool CanBeApplied(Order order) 
    {
        return
            order.IsLargeOrder &&
            order.OrderType == OrderType.Repair &&
            order.IsNewCustomer;
    }

    public OrderStatus Evaluate(Order order)
    {
        return OrderStatus.Closed;
    }
}
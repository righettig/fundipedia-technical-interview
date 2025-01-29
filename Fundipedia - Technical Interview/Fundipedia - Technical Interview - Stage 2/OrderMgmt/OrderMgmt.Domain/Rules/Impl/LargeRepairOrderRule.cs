using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class LargeRepairOrderRule : IOrderRule
{
    public int Priority => 3;

    public bool CanBeApplied(Order order)
    {
        return order.IsLargeOrder && order.OrderType == OrderType.Repair;
    }

    public OrderStatus Evaluate(Order order)
    {
        return OrderStatus.AuthorisationRequired;
    }
}
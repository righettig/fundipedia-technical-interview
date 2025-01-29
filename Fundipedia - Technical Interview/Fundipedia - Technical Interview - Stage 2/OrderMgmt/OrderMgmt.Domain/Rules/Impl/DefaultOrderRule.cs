using OrderMgmt.Domain.Rules.Interfaces;

namespace OrderMgmt.Domain.Rules.Impl;

public class DefaultOrderRule : IOrderRule
{
    public OrderStatus Evaluate(Order orderRequest)
    {
        return OrderStatus.Confirmed;
    }
}
using OrderMgmt.Domain.Rules.Interfaces;
using OrderMgmt.Domain.Services.Interfaces;

namespace OrderMgmt.Domain.Services.Impl;

/// <summary>
/// Process order rules based on priority.
/// If no rules are applied then the default "Confirmed" rule is applied.
/// </summary>
public class OrderProcessor(IEnumerable<IOrderRule> rules) : IOrderProcessor // TODO: check for overlapping rules based on "priority"
{
    public OrderStatus DetermineOrderStatus(Order order)
    {
        foreach (var rule in rules)
        {
            if (rule.CanBeApplied(order)) 
            {
                return rule.Evaluate(order);
            }
        }

        return OrderStatus.Confirmed; // Default fallback if no rules matched
    }
}

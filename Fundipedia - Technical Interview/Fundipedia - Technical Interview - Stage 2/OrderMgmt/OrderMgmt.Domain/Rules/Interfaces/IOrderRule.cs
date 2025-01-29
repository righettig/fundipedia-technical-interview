namespace OrderMgmt.Domain.Rules.Interfaces;

/// <summary>
/// Represents an order processing rule.
/// </summary>
public interface IOrderRule
{
    // TODO: make sure the order rule processor checks that there are no overlapping rules based on priority.
    // Alternatively priority can be set by adding a rule before another in the order rule processor.
    public int Priority { get; }

    bool CanBeApplied(Order order);

    OrderStatus Evaluate(Order order);
}

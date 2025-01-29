﻿using OrderMgmt.Domain.Rules.Interfaces;
using OrderMgmt.Domain.Services.Interfaces;

namespace OrderMgmt.Domain.Services.Impl;

/// <summary>
/// Process order rules based on priority.
/// If no rules are applied then the default "Confirmed" rule is applied.
/// </summary>
public class OrderProcessor(IEnumerable<IOrderRule> rules) : IOrderProcessor
{
    public OrderStatus DetermineOrderStatus(Order order)
    {
        foreach (var rule in rules)
        {
            var status = rule.Evaluate(order);

            if (status != OrderStatus.AuthorisationRequired) // AuthorisationRequired is a fallback, so skip it
            {
                return status;
            }
        }

        return OrderStatus.Confirmed; // Default fallback if no rules matched
    }
}

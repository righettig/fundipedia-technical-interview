# Fundipedia - Technical Test

We would like you to create a simple solution in C# that works with customers and vehicle repair orders. The solution should be compromised of:
- A web api with one endpoint that processes a request
- A basic front end where the request can be submitted. Ideally this would use a framework like Vue.js but vanilla HTML and JavaScript is fine.

Outside of the desired functionality, described below, we'll also be looking to see how your solution is structured so whilst there may not be much code please bear this in mind. 

## Requirements

Based on a number of requirements which are detailed below, the status of an Order may be determined to be `Confirmed`, `Closed` or `AuthorisationRequired`.

Your endpoint should accept the inputs below, and return the appropriate status. You will also need to create unit tests to prove that it works correctly.

In the future it is likely that the requirements or possible statuses may change, expand or become more complex.

Your solution should take this into account and be easy to modify when these changes occur with minimal code changes.

In particular this might mean avoiding the use of "if" and "switch" statements which deal with specific cases and instead taking a more generic approach.


## Inputs:

```c#

IsRushOrder: bool
OrderType: OrderType
IsNewCustomer: bool
IsLargeOrder: bool

```

```c#
public enum OrderType
{
  Repair, 
  Hire,
}
```

## Outputs:

```c#
public enum OrderStatus
{
  Confirmed, 
  Closed, 
  AuthorisationRequired
}
```

## Requirements

Applied in priority order from top to bottom:

- Large repair orders for new customers should be closed
- Large rush hire orders should always be closed
- Large repair orders always require authorisation
- All rush orders for new customers always require authorisation
- All other orders should be confirmed
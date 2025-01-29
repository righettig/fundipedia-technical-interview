namespace OrderMgmt.Domain;

public class Order
{
    public bool IsRushOrder { get; set; }
    public required OrderType OrderType { get; set; }
    public bool IsNewCustomer { get; set; }
    public bool IsLargeOrder { get; set; }
}

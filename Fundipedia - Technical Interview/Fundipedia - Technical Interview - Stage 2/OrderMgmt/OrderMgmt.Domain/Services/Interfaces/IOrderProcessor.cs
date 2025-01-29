namespace OrderMgmt.Domain.Services.Interfaces;

public interface IOrderProcessor
{
    OrderStatus DetermineOrderStatus(Order order);
}

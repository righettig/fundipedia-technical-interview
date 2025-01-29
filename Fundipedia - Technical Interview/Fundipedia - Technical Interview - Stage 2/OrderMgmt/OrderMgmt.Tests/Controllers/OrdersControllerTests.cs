using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderMgmt.Api.Controllers;
using OrderMgmt.Domain;
using OrderMgmt.Domain.Services.Interfaces;

namespace OrderMgmt.Tests.Controllers;

public class OrdersControllerTests
{
    private readonly Mock<IOrderProcessor> _mockOrderProcessor;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _mockOrderProcessor = new Mock<IOrderProcessor>();
        _controller = new OrdersController(_mockOrderProcessor.Object);
    }

    [Fact]
    public void ProcessOrder_ShouldReturnOk_WhenOrderIsValid()
    {
        // Arrange
        var order = new Order { OrderType = OrderType.Repair };
        var expectedStatus = OrderStatus.Confirmed;

        _mockOrderProcessor.Setup(op => op.DetermineOrderStatus(order)).Returns(expectedStatus);

        // Act
        var result = _controller.ProcessOrder(order);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        var actualStatus = Assert.IsType<OrderStatus>(okResult.Value);
        Assert.Equal(expectedStatus, actualStatus);
    }

    [Fact]
    public void ProcessOrder_ShouldReturnBadRequest_WhenOrderIsNull()
    {
        // Act
        var result = _controller.ProcessOrder(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result as BadRequestObjectResult);
        Assert.Equal("Invalid order request.", badRequestResult.Value);
    }
}

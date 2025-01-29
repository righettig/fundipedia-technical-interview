using Fundipedia.TechnicalInterview.Controllers;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class SuppliersControllerTests
    {
        private readonly Mock<ISupplierService> _mockSupplierService;
        private readonly SuppliersController _controller;

        public SuppliersControllerTests()
        {
            _mockSupplierService = new Mock<ISupplierService>();
            _controller = new SuppliersController(_mockSupplierService.Object);
        }

        #region GetSuppliers

        [Fact]
        public async Task GetSupplier_ReturnsSuppliersList()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var suppliers = new List<Supplier> { new Supplier { Id = expectedId, FirstName = "Supplier A" } };
            _mockSupplierService.Setup(s => s.GetSuppliers()).ReturnsAsync(suppliers);

            // Act
            var result = await _controller.GetSupplier();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Supplier>>>(result);
            var returnValue = Assert.IsType<List<Supplier>>(actionResult.Value);
            Assert.Single(returnValue);

            Assert.Equal(expectedId, returnValue[0].Id);
            Assert.Equal("Supplier A", returnValue[0].FirstName);
        }

        #endregion

        #region GetSupplier

        [Fact]
        public async Task GetSupplier_ById_ReturnsSupplier()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var supplier = new Supplier { Id = expectedId, FirstName = "Supplier A" };
            _mockSupplierService.Setup(s => s.GetSupplier(expectedId)).ReturnsAsync(supplier);

            // Act
            var result = await _controller.GetSupplier(expectedId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Supplier>>(result);
            var returnValue = Assert.IsType<Supplier>(actionResult.Value);
            Assert.Equal(expectedId, returnValue.Id);
            Assert.Equal("Supplier A", returnValue.FirstName);
        }

        [Fact]
        public async Task GetSupplier_ById_ReturnsNotFound_WhenSupplierDoesNotExist()
        {
            // Arrange
            _mockSupplierService.Setup(s => s.GetSupplier(It.IsAny<Guid>())).ReturnsAsync((Supplier)null);

            // Act
            var result = await _controller.GetSupplier(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region PostSupplier

        [Fact]
        public async Task PostSupplier_ReturnsCreatedResponse()
        {
            // Arrange
            var supplier = new Supplier { Id = Guid.NewGuid(), FirstName = "Supplier A" };
            _mockSupplierService.Setup(s => s.InsertSupplier(supplier)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostSupplier(supplier);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetSupplier", actionResult.ActionName);
            var returnValue = Assert.IsType<Supplier>(actionResult.Value);
            Assert.Equal(supplier.Id, returnValue.Id);
        }

        [Fact]
        public async Task PostSupplier_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("FirstName", "FirstName cannot be longer than 64 characters.");

            // Act
            var result = await _controller.PostSupplier(new Supplier());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion

        #region DeleteSupplier

        [Fact]
        public async Task DeleteSupplier_ReturnsDeletedSupplier()
        {
            // Arrange
            var supplierId = Guid.NewGuid();
            var supplier = new Supplier { Id = supplierId, FirstName = "Supplier A" };
            _mockSupplierService.Setup(s => s.DeleteSupplier(supplierId)).ReturnsAsync(supplier);

            // Act
            var result = await _controller.DeleteSupplier(supplierId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Supplier>>(result);
            var returnValue = Assert.IsType<Supplier>(actionResult.Value);
            Assert.Equal(supplierId, returnValue.Id);
        }

        #endregion
    }
}
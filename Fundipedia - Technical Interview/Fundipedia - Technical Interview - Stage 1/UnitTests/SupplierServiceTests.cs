using Fundipedia.TechnicalInterview.Data.Context;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class SupplierServiceTests
    {
        #region GetSupplier

        [Fact]
        public async Task GetSupplier_ReturnsSupplier_WhenSupplierExists()
        {
            // Arrange
            using var context = GetDbContext();
            var supplier = new Supplier { Id = Guid.NewGuid(), FirstName = "Test Supplier" };
            context.Suppliers.Add(supplier);
            await context.SaveChangesAsync();
            var service = new SupplierService(context);

            // Act
            var result = await service.GetSupplier(supplier.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(supplier.Id, result.Id);
        }

        [Fact]
        public async Task GetSupplier_ReturnsNull_WhenSupplierDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();

            // Act
            var service = new SupplierService(context);
            var result = await service.GetSupplier(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region GetSuppliers

        [Fact]
        public async Task GetSuppliers_ReturnsAllSuppliers()
        {
            // Arrange
            using var context = GetDbContext();
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = Guid.NewGuid(), FirstName = "Supplier 1" },
                new Supplier { Id = Guid.NewGuid(), FirstName = "Supplier 2" }
            };
            context.Suppliers.AddRange(suppliers);
            await context.SaveChangesAsync();
            var service = new SupplierService(context);

            // Act
            var result = await service.GetSuppliers();

            // Assert
            Assert.Equal(2, result.Count);
        }

        #endregion

        #region InsertSupplier

        [Fact]
        public async Task InsertSupplier_AddsSupplierToDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new SupplierService(context);
            var supplier = new Supplier { Id = Guid.NewGuid(), FirstName = "New Supplier" };

            // Act
            await service.InsertSupplier(supplier);
            var result = await context.Suppliers.FindAsync(supplier.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(supplier.Id, result.Id);
        }

        #endregion

        #region DeleteSupplier

        [Fact]
        public async Task DeleteSupplier_RemovesSupplier_WhenSupplierIsNotActive()
        {
            // Arrange
            using var context = GetDbContext();
            var supplier = new Supplier { Id = Guid.NewGuid(), FirstName = "Inactive Supplier" };
            context.Suppliers.Add(supplier);
            await context.SaveChangesAsync();
            var service = new SupplierService(context);

            // Act
            var deletedSupplier = await service.DeleteSupplier(supplier.Id);

            // Assert
            Assert.NotNull(deletedSupplier);
            Assert.Null(await context.Suppliers.FindAsync(supplier.Id));
        }

        [Fact]
        public async Task DeleteSupplier_ThrowsException_WhenSupplierIsActive()
        {
            // Arrange
            using var context = GetDbContext();
            var supplier = new Supplier { Id = Guid.NewGuid(), FirstName = "Active Supplier", ActivationDate = DateTime.UtcNow.AddDays(1) };
            context.Suppliers.Add(supplier);
            await context.SaveChangesAsync();
            var service = new SupplierService(context);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => service.DeleteSupplier(supplier.Id));

            // Assert
            Assert.Equal($"Supplier {supplier.Id} is active, can't be deleted", exception.Message);
        }

        #endregion

        private SupplierContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SupplierContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SupplierContext(options);
        }
    }
}

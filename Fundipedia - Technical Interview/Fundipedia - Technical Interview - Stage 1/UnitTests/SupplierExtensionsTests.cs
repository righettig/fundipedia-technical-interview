using Fundipedia.TechnicalInterview.Model.Extensions;
using Fundipedia.TechnicalInterview.Model.Supplier;

namespace UnitTests
{
    public class SupplierExtensionsTests
    {
        [Fact]
        public void IsActive_ShouldReturnTrue_WhenActivationDateIsNotNull()
        {
            // Arrange
            var supplier = new Supplier { ActivationDate = DateTime.UtcNow };

            // Act
            var result = supplier.IsActive();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsActive_ShouldReturnFalse_WhenActivationDateIsNull()
        {
            // Arrange
            var supplier = new Supplier { ActivationDate = null };

            // Act
            var result = supplier.IsActive();

            // Assert
            Assert.False(result);
        }
    }

}

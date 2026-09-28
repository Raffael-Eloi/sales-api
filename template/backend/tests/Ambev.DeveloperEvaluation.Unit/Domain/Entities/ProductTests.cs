using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Valid product should return the total price")]
    public void Given_ValidProduct_When_CalculateTotal_Then_TheTotalShouldBeReturned()
    {
        // Arrange
        int quantity = 50;
        decimal price = 11.74M;

        Product product = new()
        {
            Quantity = quantity,
            Price = price
        };

        // Act
        decimal total = product.Total;

        // Assert
        Assert.Equal(587, total);
    }

    [Fact(DisplayName = "Validation should fail for invalid product quantity")]
    public void Given_InvalidProduct_When_Validate_Then_ShouldReturnInvalid()
    {
        // Arrange
        Product product = ProductTestData.GenerateValidProduct();
        product.Quantity = ProductTestData.GenerateInvalidQuantity();

        // Act
        ValidationResultDetail result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
    }
}

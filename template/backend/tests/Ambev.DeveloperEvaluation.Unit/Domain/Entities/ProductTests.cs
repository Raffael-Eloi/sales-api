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
        int quantity = 3;
        decimal price = 11.74M;

        Product product = new()
        {
            Quantity = quantity,
            Price = price
        };

        // Act
        decimal total = product.Total;

        // Assert
        Assert.Equal(35.22M, total);
    }

    [Fact(DisplayName = "Product with quantity from 4 to 9 items should have a 10% discount applied")]
    public void Given_ProductWithQuantityBetweenFourAndNine_When_CalculateTotal_Then_TenPercentDiscountShouldBeApplied()
    {
        // Arrange
        int quantity = 5;
        decimal price = 10M;

        Product product = new()
        {
            Quantity = quantity,
            Price = price
        };

        // Act
        decimal total = product.Total;

        // Assert
        Assert.Equal(45M, total);
    }

    [Fact(DisplayName = "Product with quantity from 10 to 20 items should have a 20% discount applied")]
    public void Given_ProductWithQuantityBetweenTenAndTwenty_When_CalculateTotal_Then_TwentyPercentDiscountShouldBeApplied()
    {
        // Arrange
        int quantity = 15;
        decimal price = 10M;

        Product product = new()
        {
            Quantity = quantity,
            Price = price
        };

        // Act
        decimal total = product.Total;

        // Assert
        Assert.Equal(120M, total);
    }

    [Fact(DisplayName = "Validation should fail when quantity exceeds the maximum allowed")]
    public void Given_ProductWithQuantityAboveMaximum_When_Validate_Then_ShouldReturnInvalid()
    {
        // Arrange
        Product product = ProductTestData.GenerateValidProduct();
        product.Quantity = ProductTestData.GenerateQuantityAboveMaximum();

        // Act
        ValidationResultDetail result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
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

    [Fact(DisplayName = "Validation should fail for invalid product price")]
    public void Given_InvalidProductPrice_When_Validate_Then_ShouldReturnInvalid()
    {
        // Arrange
        Product product = ProductTestData.GenerateValidProduct();
        product.Price = ProductTestData.GenerateInvalidPrice();

        // Act
        ValidationResultDetail result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
    }
}

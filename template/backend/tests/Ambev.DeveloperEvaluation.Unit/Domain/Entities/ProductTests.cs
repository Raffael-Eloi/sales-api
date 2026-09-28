using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Valid product should return the total price")]
    public void Given_ValidProduct_WhenCalculateTotal_ThenTheTotalShouldBeReturned()
    {
        // Arrange
        int quantity = 50;
        decimal price = 11.74M;
        
        var product = new Product
        {
            Quantity = quantity,
            Price = price
        };

        // Act
        decimal total = product.Total;

        // Assert
        Assert.Equal(587, total);
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Valid sale should return the total products price")]
    public void Given_ValidSale_When_CalculateTotal_Then_TheTotalShouldBeReturned()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidProduct();
        sale.Products.Add(ProductTestData.GenerateValidProduct());
        sale.Products.Add(ProductTestData.GenerateValidProduct());

        // Act
        decimal total = sale.Total;

        // Assert
        decimal expectedTotal = sale.Products.First().Total + sale.Products.Last().Total;
        Assert.Equal(expectedTotal, total);
    }

    [Fact(DisplayName = "Valid sale with discount should return the total products price minus discount")]
    public void Given_ValidSaleWithDiscount_When_CalculateTotal_Then_TheTotalMinusDiscountShouldBeReturned()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidProduct();
        sale.Products.Add(ProductTestData.GenerateValidProduct());
        sale.Products.Add(ProductTestData.GenerateValidProduct());
        decimal discount = sale.Total * 0.25M;
        sale.Discount = discount;

        // Act
        decimal total = sale.Total;

        // Assert
        decimal expectedTotal = (sale.Products.First().Total + sale.Products.Last().Total) - discount;
        Assert.Equal(expectedTotal, total);
    }
}

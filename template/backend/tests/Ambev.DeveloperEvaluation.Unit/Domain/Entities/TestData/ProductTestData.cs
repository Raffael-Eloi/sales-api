using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 100))
        .RuleFor(u => u.Price, f => f.Random.Decimal(1, 999))
        .RuleFor(u => u.Total, f => f.Random.Decimal(1, 999));

    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    public static int GenerateInvalidQuantity()
    {
        return new Faker().Random.Number(-999, -1);
    }

    public static decimal GenerateInvalidPrice()
    {
        return new Faker().Random.Decimal(-999, -1);
    }
}

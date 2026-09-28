using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.Number, f => f.Random.Number(1, 100))
        .RuleFor(u => u.CreatedAt, f => DateTime.Now)
        .RuleFor(u => u.CustomerId, f => f.Random.Guid())
        .RuleFor(u => u.BranchId, f => f.Random.Guid())
        .RuleFor(u => u.Discount, f => f.Random.Number(1, 999))
        .RuleFor(u => u.Status, f => SaleStatus.Active)
        .RuleFor(u => u.Products, f => []);

    public static Sale GenerateValidProduct()
    {
        return SaleFaker.Generate();
    }

    public static int GenerateInvalidNumber()
    {
        return new Faker().Random.Number(-999, 0);
    }

    public static decimal GenerateInvalidDiscount()
    {
        return new Faker().Random.Decimal(-999, -1);
    }
}

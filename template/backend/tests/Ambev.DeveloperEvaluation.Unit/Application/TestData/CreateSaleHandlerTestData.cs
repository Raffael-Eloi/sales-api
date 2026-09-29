using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Number, f => f.Random.Number(1, 100))
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.Discount, f => 0)
        .RuleFor(s => s.Products, f => []);

    public static CreateSaleCommand GenerateValidSale()
    {
        return createSaleHandlerFaker.Generate();
    }
}

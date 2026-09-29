using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.Number).GreaterThan(0);
        RuleFor(sale => sale.Discount).GreaterThanOrEqualTo(0);
        RuleFor(sale => sale.Discount)
            .Must((command, discount) => discount <= command.Products.Sum(p => p.Total))
            .WithMessage("Discount must not be greater than the total products price.");
    }
}

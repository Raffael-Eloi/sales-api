using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Number)
            .Must(x => x > 0)
            .WithMessage("Number must be greater than 0.");

        RuleFor(sale => sale.Discount)
            .Must(x => x >= 0)
            .WithMessage("Discount must be greater than or equal to 0.");

        RuleFor(sale => sale.Discount)
            .Must((sale, discount) => discount <= sale.Products.Sum(product => product.Total))
            .WithMessage("Discount must not be greater than the total products price.");
    }
}

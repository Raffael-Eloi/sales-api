using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Number: Must be greater than 0
    /// - Discount: Must be greater than or equal to 0
    /// - Discount: Must not be greater than the total products price
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.Number).GreaterThan(0);
        RuleFor(sale => sale.Discount).GreaterThanOrEqualTo(0);
        RuleFor(sale => sale.Discount)
            .Must((command, discount) => discount <= command.Products.Sum(p => p.Total))
            .WithMessage("Discount must not be greater than the total products price.");
    }
}

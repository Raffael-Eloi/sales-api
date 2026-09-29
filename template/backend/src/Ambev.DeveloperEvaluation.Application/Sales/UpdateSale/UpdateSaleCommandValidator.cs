using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for the sale update command.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required
    /// - Number: Must be greater than 0
    /// - Discount: Must be greater than or equal to 0
    /// - Discount: Must not be greater than the total products price
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.Id).NotEmpty();
        RuleFor(sale => sale.Number).GreaterThan(0);
        RuleFor(sale => sale.Discount).GreaterThanOrEqualTo(0);
        RuleFor(sale => sale.Discount)
            .Must((command, discount) => discount <= command.Products.Sum(p => p.Total))
            .WithMessage("Discount must not be greater than the total products price.");
    }
}

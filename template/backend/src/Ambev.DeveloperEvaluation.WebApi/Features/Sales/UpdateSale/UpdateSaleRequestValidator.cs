using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest that defines validation rules for sale updates.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Number).GreaterThan(0);
        RuleFor(sale => sale.Discount).GreaterThanOrEqualTo(0);
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Quantity)
            .Must(x => x >= 0)
            .WithMessage("Quantity must be greater than 1.");

        RuleFor(product => product.Quantity)
            .Must(x => x <= 20)
            .WithMessage("It's not possible to sell above 20 identical items.");

        RuleFor(product => product.Price)
            .Must(x => x >= 0)
            .WithMessage("Price must be greater than 0.");
    }
}

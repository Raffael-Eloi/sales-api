using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPercentage
    {
        get
        {
            if (Quantity >= 10 && Quantity <= 20)
                return 0.20M;

            if (Quantity >= 4)
                return 0.10M;

            return 0M;
        }
    }

    public decimal Total
    {
        get
        {
            return Quantity * Price * (1 - DiscountPercentage);
        }
    }

    public ValidationResultDetail Validate()
    {
        ProductValidator validator = new();
        ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

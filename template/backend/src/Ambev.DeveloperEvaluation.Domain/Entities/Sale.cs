using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system, including its products and totals.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the sale number.
    /// Must be greater than 0.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets the date and time when the sale was made.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the customer who made the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets the unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets the discount applied to the sale.
    /// Must not be negative and must not exceed the total price of the products.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets the sale's current status.
    /// Indicates whether the sale is active or cancelled.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets the products included in the sale.
    /// </summary>
    public List<Product> Products { get; set; } = [];

    /// <summary>
    /// Gets the total amount of the sale, calculated as the sum of the
    /// products' totals minus the applied discount.
    /// </summary>
    public decimal Total
    {
        get
        {
            return Products.Sum(x => x.Total) - Discount;
        }
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        SaleValidator validator = new();
        ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Cancels the sale.
    /// Changes the sale's status to Cancelled.
    /// </summary>
    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
    }
}

using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// API response model for UpdateSale operation
/// </summary>
public class UpdateSaleResponse
{
    /// <summary>
    /// The unique identifier of the updated sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The date and time when the sale was made
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The unique identifier of the customer who made the sale
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale was made
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The discount applied to the sale
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// The current status of the sale
    /// </summary>
    public SaleStatus Status { get; set; }
}

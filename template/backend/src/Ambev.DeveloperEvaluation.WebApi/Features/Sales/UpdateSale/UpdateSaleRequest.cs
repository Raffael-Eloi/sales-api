using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update an existing sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the customer who made the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the sale.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the products included in the sale.
    /// </summary>
    public List<Product> Products { get; set; } = [];
}

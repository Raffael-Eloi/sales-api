using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

// TODO: Document entity and properties
public class Sale : BaseEntity
{
    public int Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CustomerId { get; set; }
    
    public Guid BranchId { get; set; }

    public decimal Discount { get; set; }

    public SaleStatus Status { get; set; }

    public List<Product> Products { get; set; } = [];

    public decimal Total
    {
        get
        {
            return Products.Sum(x => x.Total) - Discount;
        }
    }
}

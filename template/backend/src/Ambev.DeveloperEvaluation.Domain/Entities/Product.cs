using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total
    {
        get
        {
            return Quantity * Price;
        }
    }
}

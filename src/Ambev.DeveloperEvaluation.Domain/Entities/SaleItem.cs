using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// The external identifier for the product.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// The name of the product (denormalized).
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// The quantity of the product sold.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// The price of a single unit of the product.
    /// </summary>
    public Money UnitPrice { get; private set; }

    /// <summary>
    /// The discount percentage applied to this item.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// The total price for this item (Quantity * UnitPrice * (1 - Discount)).
    /// </summary>
    public Money TotalPrice { get; private set; }

    /// <summary>
    /// Foreign key to the Sale aggregate root.
    /// </summary>
    public Guid SaleId { get; private set; }

    /// <summary>
    /// Navigation property to the Sale.
    /// </summary>
    public Sale Sale { get; private set; } = null!;

    private SaleItem() { }

    /// <summary>
    /// Creates a new sale item.
    /// </summary>
    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;

        Validate();
    }

    /// <summary>
    /// Internal method to apply discount and calculate the total price.
    /// </summary>
    internal void ApplyDiscountAndCalculateTotal(decimal discount)
    {
        if (discount < 0 || discount > 1)
            throw new DomainException("Discount must be between 0 and 1.");

        Discount = discount;
        TotalPrice = (Quantity * UnitPrice) * (1 - discount);
    }

    private void Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
            throw new DomainException($"SaleItem entity validation failed: {errors}");
        }
    }
}
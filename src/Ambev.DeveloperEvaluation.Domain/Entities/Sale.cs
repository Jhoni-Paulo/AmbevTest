using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction, acting as the Aggregate Root.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// The unique, human-readable number for the sale.
    /// </summary>
    public string SaleNumber { get; private set; }

    /// <summary>
    /// The date and time the sale occurred.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// The external identifier for the customer.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// The name of the customer (denormalized).
    /// </summary>
    public string CustomerName { get; private set; }

    /// <summary>
    /// The external identifier for the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; private set; }

    /// <summary>
    /// The name of the branch (denormalized).
    /// </summary>
    public string BranchName { get; private set; }

    /// <summary>
    /// The total amount for the entire sale.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Indicates if the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    private readonly List<SaleItem> _items = new();
    /// <summary>
    /// The list of items included in the sale.
    /// </summary>
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    private Sale() { }

    public Sale(Guid customerId, string customerName, Guid branchId, string branchName)
    {
        CustomerId = customerId;
        CustomerName = customerName ?? string.Empty;
        BranchId = branchId;
        BranchName = branchName ?? string.Empty;
        Date = DateTime.UtcNow;
        SaleNumber = GenerateSaleNumber();
        IsCancelled = false;
        TotalAmount = 0;

        var validator = new SaleValidator();
        validator.Validate(this, options => options.IncludeProperties(nameof(CustomerId), nameof(BranchId)));
    }

    /// <summary>
    /// Adds an item to the sale and recalculates the total.
    /// </summary>
    public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new DomainException("Cannot add items to a cancelled sale.");

        if (quantity > 20)
            throw new DomainException("Cannot sell more than 20 units of the same item in a single transaction.");

        var item = new SaleItem(productId, productName, quantity, unitPrice);

        var discount = CalculateDiscount(quantity);
        item.ApplyDiscountAndCalculateTotal(discount);

        _items.Add(item);
        RecalculateTotalAmount();
    }

    /// <summary>
    /// Cancels the sale.
    /// </summary>
    public void Cancel()
    {
        if (IsCancelled)
            throw new DomainException("Sale is already cancelled.");

        IsCancelled = true;
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }

    private decimal CalculateDiscount(int quantity)
    {
        if (quantity >= 10) return 0.20m;
        if (quantity >= 4) return 0.10m; 
        return 0m;
    }

    private string GenerateSaleNumber()
    {
        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var randomPart = Guid.NewGuid().ToString("N")[..8].ToUpper();
        return $"{datePart}-{randomPart}";
    }

    /// <summary>
    /// Performs full entity validation before persistence.
    /// </summary>
    public void Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
            throw new DomainException($"Sale entity validation failed: {errors}");
        }
    }
}
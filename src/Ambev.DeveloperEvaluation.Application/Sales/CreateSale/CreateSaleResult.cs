namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Result of the CreateSaleCommand.
/// </summary>
public class CreateSaleResult
{
    public Guid SaleId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
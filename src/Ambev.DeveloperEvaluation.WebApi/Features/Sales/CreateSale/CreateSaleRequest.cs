namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents the request to create a new sale.
/// </summary>
public class CreateSaleRequest
{
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string? BranchName { get; set; }
    public List<SaleItemRequest> Items { get; set; } = new();

    public class SaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
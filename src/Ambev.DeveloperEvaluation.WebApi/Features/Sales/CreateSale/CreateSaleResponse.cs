namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents the response after creating a new sale.
    /// </summary>
    public class CreateSaleResponse
    {
        public Guid SaleId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}

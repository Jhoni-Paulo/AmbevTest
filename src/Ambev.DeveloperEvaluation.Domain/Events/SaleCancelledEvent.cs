namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event published when a sale is cancelled.
/// </summary>
public class SaleCancelledEvent
{
    public Guid SaleId { get; }

    public SaleCancelledEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
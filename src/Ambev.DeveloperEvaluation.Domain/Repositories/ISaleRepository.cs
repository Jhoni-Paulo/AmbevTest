using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale aggregate operations.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Adds a new sale to the repository.
    /// </summary>
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
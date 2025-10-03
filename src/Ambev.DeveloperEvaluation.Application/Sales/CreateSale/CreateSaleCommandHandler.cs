using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for the CreateSaleCommand.
/// </summary>
public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper, IPublisher publisher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale(
            request.CustomerId,
            request.CustomerName,
            request.BranchId,
            request.BranchName
        );

        foreach (var itemCommand in request.Items)
        {
            sale.AddItem(
                itemCommand.ProductId,
                itemCommand.ProductName,
                itemCommand.Quantity,
                itemCommand.UnitPrice
            );
        }

        sale.Validate();

        await _saleRepository.AddAsync(sale, cancellationToken);

        await _publisher.Publish(new SaleCreatedEvent(sale), cancellationToken);

        return _mapper.Map<CreateSaleResult>(sale);
    }
}
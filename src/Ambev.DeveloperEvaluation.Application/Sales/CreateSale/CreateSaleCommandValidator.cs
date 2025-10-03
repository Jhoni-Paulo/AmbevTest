using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for the CreateSaleCommand.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.Items).NotEmpty().WithMessage("Sale must have at least one item.");

        RuleForEach(x => x.Items).SetValidator(new SaleItemCommandValidator());
    }
}

/// <summary>
/// Validator for the items within the CreateSaleCommand.
/// </summary>
public class SaleItemCommandValidator : AbstractValidator<CreateSaleCommand.SaleItemCommand>
{
    public SaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20).WithMessage("Quantity for a single item cannot exceed 20.");
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
} 
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("A sale must have at least one item.");
    }
}
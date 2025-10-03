using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class CreateSaleCommandTestData
{
    private static readonly Faker<CreateSaleCommand.SaleItemCommand> SaleItemFaker =
        new Faker<CreateSaleCommand.SaleItemCommand>()
            .RuleFor(i => i.ProductId, f => f.Random.Guid())
            .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 1000));

    private static readonly Faker<CreateSaleCommand> CreateSaleCommandFaker =
        new Faker<CreateSaleCommand>()
            .RuleFor(c => c.CustomerId, f => f.Random.Guid())
            .RuleFor(c => c.CustomerName, f => f.Person.FullName)
            .RuleFor(c => c.BranchId, f => f.Random.Guid())
            .RuleFor(c => c.BranchName, f => $"Branch {f.Address.City()}")
            .RuleFor(c => c.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)));

    /// <summary>
    /// Generates a valid CreateSaleCommand with randomized data.
    /// </summary>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleCommandFaker.Generate();
    }
}
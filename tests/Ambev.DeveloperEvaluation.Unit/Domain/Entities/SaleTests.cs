using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity, focusing on its business logic.
/// </summary>
public class SaleTests
{
    private readonly Faker _faker = new();

    private decimal GetRoundedUnitPrice()
    {
        return Math.Round(_faker.Random.Decimal(10, 100), 2);
    }

    [Fact(DisplayName = "Given item quantity less than 4 When adding item Then should apply no discount")]
    public void Given_AddedItemWithQuantityLessThan4_ShouldApplyNoDiscount()
    {
        // Arrange
        var sale = new Sale(_faker.Random.Guid(), _faker.Person.FullName, _faker.Random.Guid(), "Branch");
        var quantity = _faker.Random.Int(1, 3);
        var unitPrice = GetRoundedUnitPrice();
        var expectedTotal = quantity * unitPrice;

        // Act
        sale.AddItem(_faker.Random.Guid(), _faker.Commerce.ProductName(), quantity, unitPrice);

        // Assert
        sale.TotalAmount.Should().Be(expectedTotal);
        sale.Items.First().Discount.Should().Be(0m);
    }

    [Theory(DisplayName = "Given item quantity between 4 and 9 When adding item Then should apply 10% discount")]
    [InlineData(4)]
    [InlineData(9)]
    public void AddItem_WithQuantityBetween4And9_ShouldApply10PercentDiscount(int quantity)
    {
        // Arrange
        var sale = new Sale(_faker.Random.Guid(), _faker.Person.FullName, _faker.Random.Guid(), "Branch");
        var unitPrice = GetRoundedUnitPrice();
        var expectedTotal = Math.Round((quantity * unitPrice) * 0.90m, 2);

        // Act
        sale.AddItem(_faker.Random.Guid(), _faker.Commerce.ProductName(), quantity, unitPrice);

        // Assert
        sale.TotalAmount.Should().Be(expectedTotal);
        sale.Items.First().Discount.Should().Be(0.10m);
    }

    [Theory(DisplayName = "Given item quantity of 10 or more When adding item Then should apply 20% discount")]
    [InlineData(10)]
    [InlineData(20)]
    public void Given_AddItemWithQuantityOf10OrMore_ShouldApply20PercentDiscount(int quantity)
    {
        // Arrange
        var sale = new Sale(_faker.Random.Guid(), _faker.Person.FullName, _faker.Random.Guid(), "Branch");
        var unitPrice = GetRoundedUnitPrice();
        var expectedTotal = Math.Round((quantity * unitPrice) * 0.80m, 2);

        // Act
        sale.AddItem(_faker.Random.Guid(), _faker.Commerce.ProductName(), quantity, unitPrice);

        // Assert
        sale.TotalAmount.Should().Be(expectedTotal);
        sale.Items.First().Discount.Should().Be(0.20m);
    }

    [Fact(DisplayName = "Given item quantity over 20 When adding item Then should throw DomainException")]
    public void AddItem_WithQuantityOver20_ShouldThrowDomainException()
    {
        // Arrange
        var sale = new Sale(_faker.Random.Guid(), _faker.Person.FullName, _faker.Random.Guid(), "Branch");
        Action act = () => sale.AddItem(_faker.Random.Guid(), _faker.Commerce.ProductName(), 21, _faker.Random.Decimal(10, 100));

        // Act & Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Cannot sell more than 20 units of the same item in a single transaction.");
    }

    [Fact(DisplayName = "Given a cancelled sale When adding item Then should throw DomainException")]
    public void AddItem_ToCancelledSale_ShouldThrowDomainException()
    {
        // Arrange
        var sale = new Sale(_faker.Random.Guid(), _faker.Person.FullName, _faker.Random.Guid(), "Branch");
        sale.Cancel();
        Action act = () => sale.AddItem(_faker.Random.Guid(), _faker.Commerce.ProductName(), 1, _faker.Random.Decimal(10, 100));

        // Act & Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Cannot add items to a cancelled sale.");
    }
}
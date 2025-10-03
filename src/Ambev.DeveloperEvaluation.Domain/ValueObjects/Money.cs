namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value, encapsulating currency and rounding rules.
/// This is an immutable Value Object.
/// </summary>
public record Money : IComparable<Money>, IComparable
{
    /// <summary>
    /// The number of decimal places for our currency.
    /// </summary>
    private const int DecimalPlaces = 2;

    /// <summary>
    /// The rounding strategy to be used.
    /// </summary>
    private const MidpointRounding RoundingStrategy = MidpointRounding.ToEven;

    /// <summary>
    /// The monetary value.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    /// Private constructor to enforce creation via factory method or implicit conversion.
    /// </summary>
    private Money(decimal value)
    {
        Value = Math.Round(value, DecimalPlaces, RoundingStrategy);
    }

    public static implicit operator Money(decimal value) => new(value);

    public static implicit operator decimal(Money money) => money.Value;

    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
    public static Money operator -(Money a, Money b) => new(a.Value - b.Value);
    public static Money operator *(Money a, decimal factor) => new(a.Value * factor);
    public static Money operator *(int factor, Money a) => new(a.Value * factor);

    public override string ToString() => Value.ToString("F2");

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    public int CompareTo(Money? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }

    /// <summary>
    /// Compares the current instance with another object.
    /// </summary>
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (obj is not Money otherMoney)
        {
            throw new ArgumentException($"Object must be of type {nameof(Money)}");
        }
        return CompareTo(otherMoney);
    }
}
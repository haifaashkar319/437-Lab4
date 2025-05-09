namespace Core.Domain.ValueObjects;

public readonly record struct BorrowerName
{
    public string Value { get; }
    public BorrowerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Borrower name must be at least 3 characters.");

        Value = value;
    }

    public override string ToString() => Value;
}
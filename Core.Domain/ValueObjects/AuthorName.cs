namespace Core.Domain.ValueObjects;

public readonly record struct AuthorName
{
    public string Value { get; }
    public AuthorName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Author name must be at least 3 characters.");

        Value = value;
    }

    public override string ToString() => Value;
}
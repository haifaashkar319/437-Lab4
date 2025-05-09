namespace Core.Domain.ValueObjects;

public readonly record struct Title
{
    public string Value { get; }
    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Title must be at least 3 characters.");
        Value = value;
    }

    public override string ToString() => Value;
}
namespace Core.Domain.ValueObjects;

public readonly record struct Genre
{
    public string Value { get; }
    public Genre(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Genre is required.");

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Za-z\s]+$"))
            throw new ArgumentException("Genre must contain only letters.");

        Value = value;
    }

    public override string ToString() => Value;
}
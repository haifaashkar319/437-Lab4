using FluentValidation;
using LibraryManagementService.Models;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.")
            .Matches(@"^[A-Za-z][A-Za-z .-]{2,49}$").WithMessage("Invalid Title.");

        RuleFor(b => b.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .Length(3, 30).WithMessage("Genre must be between 3 and 30 characters.")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Genre can only contain letters and spaces.");

        RuleFor(b => b.AuthorId)
            .GreaterThan(0).WithMessage("Valid Author is required.");
    }
}

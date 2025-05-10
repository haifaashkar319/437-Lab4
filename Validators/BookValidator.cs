using FluentValidation;
using Presentation.ViewModels;

public class BookCreateViewModelValidator : AbstractValidator<BookCreateViewModel>
{
    private readonly List<string> _validGenres = new()
{
"Fantasy", "Romance", "Horror", "Science Fiction", "Mystery", "Thriller", "Non-Fiction", "Biography", "Poetry"
};
    public BookCreateViewModelValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.")
            .Matches(@"^[A-Za-z][A-Za-z .-]{2,49}$").WithMessage("Invalid Title format.");

        RuleFor(b => b.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .Must(g => _validGenres.Contains(g)).WithMessage("Genre must be one of: " + string.Join(", ", _validGenres))
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Genre can only contain letters and spaces.");

        RuleFor(b => b.AuthorId)
            .GreaterThan(0).WithMessage("Author selection is required.");
    }
}
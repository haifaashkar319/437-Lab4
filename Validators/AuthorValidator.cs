using FluentValidation;
using LibraryManagement.ViewModels;

public class AuthorCreateViewModelValidator : AbstractValidator<AuthorCreateViewModel>
{
    public AuthorCreateViewModelValidator()
    {
        RuleFor(a => a.Name)
        .NotEmpty().WithMessage("Author name is required.")
        .Length(3, 50).WithMessage("Author name must be between 3 and 50 characters.")
        .Matches(@"^[A-Z][a-zA-Z .-]*$").WithMessage("Author name must start with a capital letter and contain only letters, spaces, hyphens, or periods.");
    }
}
using FluentValidation;
using Application.Books.Commands;

namespace Application.Books.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Genre must contain only letters");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("Valid AuthorId is required");
    }
}
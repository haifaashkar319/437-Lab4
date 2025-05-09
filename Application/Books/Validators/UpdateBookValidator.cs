using Application.Books.Commands;
using FluentValidation;

namespace Application.Books.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(b => b.Genre)
            .NotEmpty()
            .Matches(@"^[A-Za-z\s]+$");

        RuleFor(b => b.AuthorId)
            .GreaterThan(0);
    }
}

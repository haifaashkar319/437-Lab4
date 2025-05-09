using Application.Borrowers.Commands;
using FluentValidation;

namespace Application.Borrowers.Validators;

public class CreateBorrowerValidator : AbstractValidator<CreateBorrowerCommand>
{
    public CreateBorrowerValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Borrower name must be at least 3 characters.");
    }
}

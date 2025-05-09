using Application.Borrowers.Commands;
using FluentValidation;

namespace Application.Borrowers.Validators;

public class UpdateBorrowerValidator : AbstractValidator<UpdateBorrowerCommand>
{
    public UpdateBorrowerValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Borrower name must be at least 3 characters.");
    }
}

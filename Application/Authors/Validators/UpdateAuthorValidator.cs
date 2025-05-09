using Application.Authors.Commands;
using FluentValidation;

namespace Application.Authors.Validators;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Author name must be at least 3 characters.");
    }
}

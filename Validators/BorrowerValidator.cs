using FluentValidation;
using LibraryManagementService.Models;

public class BorrowerValidator : AbstractValidator<Borrower>
{
    public BorrowerValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 25).WithMessage("Name must be between 3 and 25 characters.")
            .Matches(@"^[A-Za-z][A-Za-z. \-]{2,24}$").WithMessage("Invalid Name.");
    }
}

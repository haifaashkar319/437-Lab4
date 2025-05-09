using FluentValidation;
using LibraryManagementService.Models;

public class BorrowerValidator : AbstractValidator<Borrower>
{
public BorrowerValidator()
{
RuleFor(b => b.Name)
.NotEmpty().WithMessage("Borrower name is required.")
.Length(3, 25).WithMessage("Borrower name must be between 3 and 25 characters.")
.Matches(@"^[A-Z][a-zA-Z.- ]*$")
.WithMessage("Borrower name must start with a capital letter and contain only letters, spaces, periods, or hyphens.");
}
}
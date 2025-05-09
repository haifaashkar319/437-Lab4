using FluentValidation;
using LibraryManagementService.Models;

public class LoanValidator : AbstractValidator<Loan>
{
    public LoanValidator()
    {
        RuleFor(l => l.BookId)
            .GreaterThan(0).WithMessage("Book selection is required.");

        RuleFor(l => l.BorrowerId)
            .GreaterThan(0).WithMessage("Borrower selection is required.");

        RuleFor(l => l.LoanDate)
            .NotEmpty().WithMessage("Loan date is required.")
            .LessThanOrEqualTo(DateTime.Today.AddDays(1))
            .WithMessage("Loan date cannot be in the future.");
    }
}

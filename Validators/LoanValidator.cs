using FluentValidation;
using LibraryManagementService.Models;

public class LoanValidator : AbstractValidator<Loan>
{
    public LoanValidator()
    {
        RuleFor(l => l.BookId)
        .GreaterThan(0).WithMessage("A book must be selected.");
        RuleFor(l => l.BorrowerId)
            .GreaterThan(0).WithMessage("A borrower must be selected.");

        RuleFor(l => l.LoanDate)
            .NotEmpty().WithMessage("Loan date is required.")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Loan date cannot be in the future.");
    }
}

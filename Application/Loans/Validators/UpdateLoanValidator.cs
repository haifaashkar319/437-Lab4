using Application.Loans.Commands;
using FluentValidation;

namespace Application.Loans.Validators;

public class UpdateLoanValidator : AbstractValidator<UpdateLoanCommand>
{
    public UpdateLoanValidator()
    {
        RuleFor(l => l.BookId).GreaterThan(0);
        RuleFor(l => l.BorrowerId).GreaterThan(0);
        RuleFor(l => l.LoanDate).NotEmpty();
    }
}

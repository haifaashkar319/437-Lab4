using MediatR;

namespace Application.Loans.Commands;

public class DeleteLoanCommand : IRequest<bool>
{
    public int LoanId { get; }

    public DeleteLoanCommand(int loanId)
    {
        LoanId = loanId;
    }
}

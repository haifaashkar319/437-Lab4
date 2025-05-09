using MediatR;

namespace Application.Loans.Commands;

public class UpdateLoanCommand : IRequest<bool>
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime LoanDate { get; set; }

    public UpdateLoanCommand() { }

    public UpdateLoanCommand(int loanId, int bookId, int borrowerId, DateTime loanDate)
    {
        LoanId = loanId;
        BookId = bookId;
        BorrowerId = borrowerId;
        LoanDate = loanDate;
    }
}

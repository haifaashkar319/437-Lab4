using MediatR;

namespace Application.Loans.Commands;

public class CreateLoanCommand : IRequest<int>
{
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime LoanDate { get; set; }

    public CreateLoanCommand() { }

    public CreateLoanCommand(int bookId, int borrowerId, DateTime loanDate)
    {
        BookId = bookId;
        BorrowerId = borrowerId;
        LoanDate = loanDate;
    }
}

namespace Core.Domain.Events;

public class LoanCreatedEvent
{
    public int LoanId { get; }
    public int BookId { get; }
    public int BorrowerId { get; }

    public LoanCreatedEvent(int loanId, int bookId, int borrowerId)
    {
        LoanId = loanId;
        BookId = bookId;
        BorrowerId = borrowerId;
    }
}

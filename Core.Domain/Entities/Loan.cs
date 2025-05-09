namespace Core.Domain.Entities;

public class Loan
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime LoanDate { get; set; }

    public Book? Book { get; set; }
    public Borrower? Borrower { get; set; }
}
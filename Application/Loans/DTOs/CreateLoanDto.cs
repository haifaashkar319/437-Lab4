namespace Application.Loans.DTOs;

public class CreateLoanDto
{
    public int BorrowerId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}

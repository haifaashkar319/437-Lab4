namespace Application.Loans.DTOs;

public class LoanDto
{
    public int LoanId { get; set; }
    public int BorrowerId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}

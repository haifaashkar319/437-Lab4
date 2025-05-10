namespace Application.Borrowers.DTOs;

public class UpdateBorrowerDto
{
    public int BorrowerId { get; set; }
    public string Name { get; set; } = string.Empty;
}

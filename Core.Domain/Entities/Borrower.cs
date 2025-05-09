using Core.Domain.ValueObjects;

namespace Core.Domain.Entities;

public class Borrower
{
    public int BorrowerId { get; set; }
    public BorrowerName Name { get; set; } 

    public ICollection<Loan>? Loans { get; set; }
}
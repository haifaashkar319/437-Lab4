namespace Core.Domain.Entities;
using Core.Domain.ValueObjects;

public class Book
{
    public int BookId { get; set; }
    public Title Title { get; set; }
    public int AuthorId { get; set; }
    public Genre Genre { get; set; } 

    public Author? Author { get; set; }
    public ICollection<Loan>? Loans { get; set; }
}
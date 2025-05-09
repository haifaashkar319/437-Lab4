namespace Core.Domain.Entities;
using Core.Domain.ValueObjects;

public class Author
{
    public int AuthorId { get; set; }
    public AuthorName Name { get; set; } 

    public ICollection<Book>? Books { get; set; }
}
namespace Application.Books.DTOs;

public class UpdateBookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
}

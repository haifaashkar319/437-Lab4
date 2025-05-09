using MediatR;

namespace Application.Books.Commands;

public class UpdateBookCommand : IRequest<bool>
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int AuthorId { get; set; }

    public UpdateBookCommand() { }

    public UpdateBookCommand(int bookId, string title, string genre, int authorId)
    {
        BookId = bookId;
        Title = title;
        Genre = genre;
        AuthorId = authorId;
    }
}

using MediatR;

namespace Application.Books.Commands;

public class DeleteBookCommand : IRequest<bool>
{
    public int BookId { get; }

    public DeleteBookCommand(int bookId)
    {
        BookId = bookId;
    }
}

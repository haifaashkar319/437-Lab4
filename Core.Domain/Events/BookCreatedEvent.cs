namespace Core.Domain.Events;

public class BookCreatedEvent
{
    public int BookId { get; }
    public string Title { get; }

    public BookCreatedEvent(int bookId, string title)
    {
        BookId = bookId;
        Title = title;
    }
}
// this is just the plain event object that represents the event of a book being created.
// it contains the properties that are relevant to the event, such as BookId and Title.
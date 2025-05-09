namespace Core.Domain.Events;

public class AuthorCreatedEvent
{
    public int AuthorId { get; }
    public string Name { get; }

    public AuthorCreatedEvent(int authorId, string name)
    {
        AuthorId = authorId;
        Name = name;
    }
}
// this is just the plain event object that represents the event of an author being created.
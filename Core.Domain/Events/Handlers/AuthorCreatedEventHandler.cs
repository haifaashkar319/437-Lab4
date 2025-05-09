using Core.Domain.Events;

namespace Core.Domain.Events.Handlers;

public class AuthorCreatedEventHandler
{
    public void Handle(AuthorCreatedEvent @event)
    {
        Console.WriteLine($"✍️ AuthorCreatedEvent handled → ID: {@event.AuthorId}, Name: {@event.Name}");
    }
}
// This is the event handler for the AuthorCreatedEvent. It handles the event when an author is created.
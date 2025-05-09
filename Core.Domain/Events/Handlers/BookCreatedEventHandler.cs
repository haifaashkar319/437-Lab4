using System;

namespace Core.Domain.Events.Handlers;

public class BookCreatedEventHandler
{
    public void Handle(BookCreatedEvent @event)
    {
        Console.WriteLine($"📚 BookCreatedEvent handled → ID: {@event.BookId}, Title: {@event.Title}");
        // You could later replace this with logging, audit, notification, etc.
    }
}

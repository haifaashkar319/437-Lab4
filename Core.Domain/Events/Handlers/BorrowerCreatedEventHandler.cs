using Core.Domain.Events;

namespace Core.Domain.Events.Handlers;

public class BorrowerCreatedEventHandler
{
    public void Handle(BorrowerCreatedEvent @event)
    {
        Console.WriteLine($"👤 BorrowerCreatedEvent handled → ID: {@event.BorrowerId}, Name: {@event.Name}");
    }
}
// this is the event handler for the BorrowerCreatedEvent. It handles the event when a borrower is created.
using Core.Domain.Events;

namespace Core.Domain.Events.Handlers;

public class LoanCreatedEventHandler
{
    public void Handle(LoanCreatedEvent @event)
    {
        Console.WriteLine($"📄 LoanCreatedEvent handled → Loan ID: {@event.LoanId}, Book ID: {@event.BookId}, Borrower ID: {@event.BorrowerId}");
    }
}
// This is the event handler for the LoanCreatedEvent. It handles the event when a loan is created.
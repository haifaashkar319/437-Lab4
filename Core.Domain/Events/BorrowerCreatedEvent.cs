namespace Core.Domain.Events;

public class BorrowerCreatedEvent
{
    public int BorrowerId { get; }
    public string Name { get; }

    public BorrowerCreatedEvent(int borrowerId, string name)
    {
        BorrowerId = borrowerId;
        Name = name;
    }
}

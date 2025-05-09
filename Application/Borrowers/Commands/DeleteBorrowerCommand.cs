using MediatR;

namespace Application.Borrowers.Commands;

public class DeleteBorrowerCommand : IRequest<bool>
{
    public int BorrowerId { get; }

    public DeleteBorrowerCommand(int borrowerId)
    {
        BorrowerId = borrowerId;
    }
}

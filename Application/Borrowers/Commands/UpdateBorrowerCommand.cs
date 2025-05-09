using MediatR;

namespace Application.Borrowers.Commands;

public class UpdateBorrowerCommand : IRequest<bool>
{
    public int BorrowerId { get; set; }
    public string Name { get; set; } = string.Empty;

    public UpdateBorrowerCommand() { }

    public UpdateBorrowerCommand(int borrowerId, string name)
    {
        BorrowerId = borrowerId;
        Name = name;
    }
}

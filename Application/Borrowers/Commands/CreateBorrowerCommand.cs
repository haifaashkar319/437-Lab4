using MediatR;

namespace Application.Borrowers.Commands;

public class CreateBorrowerCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public CreateBorrowerCommand() { }

    public CreateBorrowerCommand(string name)
    {
        Name = name;
    }
}

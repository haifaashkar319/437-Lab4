using MediatR;

namespace Application.Authors.Commands;

public class CreateAuthorCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;

    public CreateAuthorCommand() { }

    public CreateAuthorCommand(string name)
    {
        Name = name;
    }
}

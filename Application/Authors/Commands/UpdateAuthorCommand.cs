using MediatR;

namespace Application.Authors.Commands;

public class UpdateAuthorCommand : IRequest<bool>
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;

    public UpdateAuthorCommand() { }

    public UpdateAuthorCommand(int authorId, string name)
    {
        AuthorId = authorId;
        Name = name;
    }
}

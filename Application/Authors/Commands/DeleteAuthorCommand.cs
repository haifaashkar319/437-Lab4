using MediatR;

namespace Application.Authors.Commands;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int AuthorId { get; }

    public DeleteAuthorCommand(int authorId)
    {
        AuthorId = authorId;
    }
}

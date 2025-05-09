using Core.Domain.Interfaces;
using MediatR;
using Application.Authors.Commands;

namespace Application.Authors.Handlers;

public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IAuthorRepository _repository;

    public DeleteAuthorHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.AuthorId);
        if (author == null) return false;

        await _repository.DeleteAsync(author);
        return true;
    }
}

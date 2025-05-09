using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using MediatR;
using Application.Authors.Commands;

namespace Application.Authors.Handlers;

public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, bool>
{
    private readonly IAuthorRepository _repository;

    public UpdateAuthorHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.AuthorId);
        if (author == null) return false;

        author.Name = new AuthorName(request.Name);

        await _repository.UpdateAsync(author);
        return true;
    }
}

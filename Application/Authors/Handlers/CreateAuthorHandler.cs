using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using Core.Domain.Events;
using Core.Domain.Events.Handlers;
using MediatR;
using Application.Authors.Commands;

namespace Application.Authors.Handlers;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IAuthorRepository _repository;

    public CreateAuthorHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = new AuthorName(request.Name)
        };

        await _repository.AddAsync(author);

        var @event = new AuthorCreatedEvent(author.AuthorId, author.Name.Value);
        new AuthorCreatedEventHandler().Handle(@event);

        return author.AuthorId;
    }
}

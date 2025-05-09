using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Authors.Queries;

namespace Application.Authors.Handlers;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Author?>
{
    private readonly IAuthorRepository _repository;

    public GetAuthorByIdHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

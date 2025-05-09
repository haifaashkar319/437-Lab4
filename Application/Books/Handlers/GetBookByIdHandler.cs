using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Books.Queries;

namespace Application.Books.Handlers;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _repository;

    public GetBookByIdHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdWithAuthorAsync(request.Id);
    }
}

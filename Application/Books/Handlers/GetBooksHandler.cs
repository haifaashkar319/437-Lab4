using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Books.Queries;

namespace Application.Books.Handlers;

public class GetBooksHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _repository;

    public GetBooksHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var allBooks = await _repository.GetAllWithAuthorAsync();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            return allBooks.Where(b =>
                b.Title.Value.ToLower().Contains(search) ||
                b.Author.Name.Value.ToLower().Contains(search));
        }

        return allBooks;
    }
}

using Application.Books.Commands;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using MediatR;

namespace Application.Books.Handlers;

public class CreateBookHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IBookRepository _repo;

    public CreateBookHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken ct)
    {
        var book = new Book
        {
            Title = new Title(request.Title),
            Genre = new Genre(request.Genre),
            AuthorId = request.AuthorId
        };

        await _repo.AddAsync(book);
        return book.BookId;
    }
}
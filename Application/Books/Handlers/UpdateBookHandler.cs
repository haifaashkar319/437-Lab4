using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using MediatR;
using Application.Books.Commands;

namespace Application.Books.Handlers;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, bool>
{
    private readonly IBookRepository _repository;

    public UpdateBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.BookId);
        if (book == null) return false;

        book.Title = new Title(request.Title);
        book.Genre = new Genre(request.Genre);
        book.AuthorId = request.AuthorId;

        await _repository.UpdateAsync(book);
        return true;
    }
}

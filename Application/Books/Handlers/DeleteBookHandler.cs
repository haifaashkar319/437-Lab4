using Core.Domain.Interfaces;
using MediatR;
using Application.Books.Commands;

namespace Application.Books.Handlers;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _repository;

    public DeleteBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.BookId);
        if (book == null) return false;

        await _repository.DeleteAsync(request.BookId);
        return true;
    }
}

using MediatR;

namespace Application.Books.Commands;

public record CreateBookCommand(string Title, string Genre, int AuthorId) : IRequest<int>;
using Core.Domain.Entities;
using MediatR;

namespace Application.Books.Queries;

public class GetBookByIdQuery : IRequest<Book?>
{
    public int Id { get; }

    public GetBookByIdQuery(int id)
    {
        Id = id;
    }
}

using Application.Books.DTOs;
using MediatR;

namespace Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<BookDto?>
    {
        public int Id { get; }
        public GetBookByIdQuery(int id) => Id = id;
    }
}

using Application.Borrowers.DTOs;
using MediatR;

namespace Application.Borrowers.Queries
{
    public class GetBorrowerByIdQuery : IRequest<BorrowerDto?>
    {
        public int Id { get; }
        public GetBorrowerByIdQuery(int id) => Id = id;
    }
}

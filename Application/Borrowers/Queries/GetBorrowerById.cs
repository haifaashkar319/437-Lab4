using Core.Domain.Entities;
using MediatR;

namespace Application.Borrowers.Queries;

public class GetBorrowerByIdQuery : IRequest<Borrower?>
{
    public int Id { get; }

    public GetBorrowerByIdQuery(int id)
    {
        Id = id;
    }
}

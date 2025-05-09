using Core.Domain.Entities;
using MediatR;

namespace Application.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<Author?>
{
    public int Id { get; }

    public GetAuthorByIdQuery(int id)
    {
        Id = id;
    }
}

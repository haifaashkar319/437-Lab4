using Core.Domain.Entities;
using Application.Authors.DTOs;
using MediatR;

namespace Application.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<AuthorDto?>
{
    public int Id { get; }

    public GetAuthorByIdQuery(int id)
    {
        Id = id;
    }
}

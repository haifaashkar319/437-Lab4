using Application.Authors.DTOs;
using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Authors.Queries;

public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
    public string? Search { get; set; }

    public GetAuthorsQuery(string? search = null)
    {
        Search = search;
    }
}

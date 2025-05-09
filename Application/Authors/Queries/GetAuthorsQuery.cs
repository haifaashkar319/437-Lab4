using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Authors.Queries;

public class GetAuthorsQuery : IRequest<IEnumerable<Author>>
{
    public string? Search { get; set; }

    public GetAuthorsQuery(string? search = null)
    {
        Search = search;
    }
}

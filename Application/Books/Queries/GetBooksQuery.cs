using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Books.Queries;

public class GetBooksQuery : IRequest<IEnumerable<Book>>
{
    public string? Search { get; set; }

    public GetBooksQuery(string? search = null)
    {
        Search = search;
    }
}

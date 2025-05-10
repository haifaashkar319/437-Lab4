using Application.Books.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Books.Queries
{
    public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
    {
        public string? Search { get; set; }
        public GetBooksQuery(string? search = null) => Search = search;
    }
}

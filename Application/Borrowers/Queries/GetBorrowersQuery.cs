using Application.Borrowers.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Borrowers.Queries
{
    public class GetBorrowersQuery : IRequest<IEnumerable<BorrowerDto>>
    {
        public string? Search { get; set; }
        public GetBorrowersQuery(string? search = null) => Search = search;
    }
}

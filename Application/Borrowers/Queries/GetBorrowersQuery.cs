using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Borrowers.Queries;

public class GetBorrowersQuery : IRequest<IEnumerable<Borrower>>
{
    public string? Search { get; set; }

    public GetBorrowersQuery(string? search = null)
    {
        Search = search;
    }
}

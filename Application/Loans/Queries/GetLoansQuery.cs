using Application.Loans.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Loans.Queries;

public class GetLoansQuery : IRequest<IEnumerable<LoanDto>>
{
    public string? Search { get; set; }
    public GetLoansQuery(string? search = null) => Search = search;
}

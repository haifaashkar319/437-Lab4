using Core.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Loans.Queries;

public class GetLoansQuery : IRequest<IEnumerable<Loan>>
{
    public GetLoansQuery() { }
}

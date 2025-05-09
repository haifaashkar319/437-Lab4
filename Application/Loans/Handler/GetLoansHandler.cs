using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Loans.Queries;

namespace Application.Loans.Handlers;

public class GetLoansHandler : IRequestHandler<GetLoansQuery, IEnumerable<Loan>>
{
    private readonly ILoanRepository _repository;

    public GetLoansHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Loan>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllWithIncludesAsync();
    }
}

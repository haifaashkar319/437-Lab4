using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Loans.Queries;

namespace Application.Loans.Handlers;

public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, Loan?>
{
    private readonly ILoanRepository _repository;

    public GetLoanByIdHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<Loan?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

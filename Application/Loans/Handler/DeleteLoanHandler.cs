using Core.Domain.Interfaces;
using MediatR;
using Application.Loans.Commands;

namespace Application.Loans.Handlers;

public class DeleteLoanHandler : IRequestHandler<DeleteLoanCommand, bool>
{
    private readonly ILoanRepository _repository;

    public DeleteLoanHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetByIdAsync(request.LoanId);
        if (loan == null) return false;

        await _repository.DeleteAsync(loan);
        return true;
    }
}

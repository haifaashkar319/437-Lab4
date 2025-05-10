using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Loans.Commands;

namespace Application.Loans.Handlers;

public class UpdateLoanHandler : IRequestHandler<UpdateLoanCommand, bool>
{
    private readonly ILoanRepository _repository;

    public UpdateLoanHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetByIdAsync(request.LoanId);
        if (loan == null) return false;

        loan.BookId = request.BookId;
        loan.BorrowerId = request.BorrowerId;
        loan.LoanDate = request.LoanDate;

        await _repository.UpdateAsync(loan);
        return true;
    }
}

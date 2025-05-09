using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Loans.Commands;
using Core.Domain.Events;
using Core.Domain.Events.Handlers;

namespace Application.Loans.Handlers;

public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, int>
{
    private readonly ILoanRepository _repository;

    public CreateLoanHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Loan
        {
            BookId = request.BookId,
            BorrowerId = request.BorrowerId,
            LoanDate = request.LoanDate
        };

        await _repository.AddAsync(loan);

        var @event = new LoanCreatedEvent(loan.LoanId, loan.BookId, loan.BorrowerId);
        new LoanCreatedEventHandler().Handle(@event);

        return loan.LoanId;
    }
}

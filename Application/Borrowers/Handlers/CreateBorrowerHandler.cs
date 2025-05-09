using Core.Domain.Entities;
using Core.Domain.Events.Handlers;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using MediatR;
using Application.Borrowers.Commands;
using Core.Domain.Events;

namespace Application.Borrowers.Handlers;

public class CreateBorrowerHandler : IRequestHandler<CreateBorrowerCommand, int>
{
    private readonly IBorrowerRepository _repository;

    public CreateBorrowerHandler(IBorrowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateBorrowerCommand request, CancellationToken cancellationToken)
    {
        var borrower = new Borrower
        {
            Name = new BorrowerName(request.Name)
        };

        await _repository.AddAsync(borrower);

        var @event = new BorrowerCreatedEvent(borrower.BorrowerId, borrower.Name.Value);
        new BorrowerCreatedEventHandler().Handle(@event);

        return borrower.BorrowerId;
    }
}

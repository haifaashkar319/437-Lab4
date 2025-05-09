using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.ValueObjects;
using MediatR;
using Application.Borrowers.Commands;

namespace Application.Borrowers.Handlers;

public class UpdateBorrowerHandler : IRequestHandler<UpdateBorrowerCommand, bool>
{
    private readonly IBorrowerRepository _repository;

    public UpdateBorrowerHandler(IBorrowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateBorrowerCommand request, CancellationToken cancellationToken)
    {
        var borrower = await _repository.GetByIdAsync(request.BorrowerId);
        if (borrower == null) return false;

        borrower.Name = new BorrowerName(request.Name);

        await _repository.UpdateAsync(borrower);
        return true;
    }
}

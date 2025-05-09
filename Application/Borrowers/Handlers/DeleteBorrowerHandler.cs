using Core.Domain.Interfaces;
using MediatR;
using Application.Borrowers.Commands;

namespace Application.Borrowers.Handlers;

public class DeleteBorrowerHandler : IRequestHandler<DeleteBorrowerCommand, bool>
{
    private readonly IBorrowerRepository _repository;

    public DeleteBorrowerHandler(IBorrowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteBorrowerCommand request, CancellationToken cancellationToken)
    {
        var borrower = await _repository.GetByIdAsync(request.BorrowerId);
        if (borrower == null) return false;

        await _repository.DeleteAsync(borrower);
        return true;
    }
}

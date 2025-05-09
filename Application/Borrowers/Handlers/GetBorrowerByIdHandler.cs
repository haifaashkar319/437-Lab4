using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Borrowers.Queries;

namespace Application.Borrowers.Handlers;

public class GetBorrowerByIdHandler : IRequestHandler<GetBorrowerByIdQuery, Borrower?>
{
    private readonly IBorrowerRepository _repository;

    public GetBorrowerByIdHandler(IBorrowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Borrower?> Handle(GetBorrowerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

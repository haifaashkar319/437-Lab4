using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Borrowers.Queries;

namespace Application.Borrowers.Handlers;

public class GetBorrowersHandler : IRequestHandler<GetBorrowersQuery, IEnumerable<Borrower>>
{
    private readonly IBorrowerRepository _repository;

    public GetBorrowersHandler(IBorrowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Borrower>> Handle(GetBorrowersQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            return all.Where(b => b.Name.Value.ToLower().Contains(search));
        }

        return all;
    }
}

using Application.Borrowers.DTOs;
using Application.Borrowers.Queries;
using Core.Domain.Interfaces;
using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Borrowers.Handlers
{
public class GetBorrowersHandler : IRequestHandler<GetBorrowersQuery, IEnumerable<BorrowerDto>>
{
    private readonly IBorrowerRepository _repository;
    private readonly IMapper _mapper;

    public GetBorrowersHandler(IBorrowerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper     = mapper;
    }

    public async Task<IEnumerable<BorrowerDto>> Handle(GetBorrowersQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            // ← Use .Value (a string) before calling ToLower()
            all = all.Where(b => 
                b.Name.Value
                 .ToLower()
                 .Contains(search)
            );
        }

        // Now map the filtered domain objects into your DTOs
        return _mapper.Map<IEnumerable<BorrowerDto>>(all);
    }
}
}

using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Authors.Queries;

namespace Application.Authors.Handlers;

public class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
{
    private readonly IAuthorRepository _repository;

    public GetAuthorsHandler(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            return all.Where(a => a.Name.Value.ToLower().Contains(search));
        }

        return all;
    }
}

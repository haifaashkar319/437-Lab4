using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Authors.Queries;
using Application.Authors.DTOs;
using AutoMapper;

namespace Application.Authors.Handlers;

public class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public GetAuthorsHandler(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            all = all.Where(a => a.Name.Value.ToLower().Contains(search));
        }

        return _mapper.Map<IEnumerable<AuthorDto>>(all);
    }
}

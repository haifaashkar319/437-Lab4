using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Authors.Queries;
using Application.Authors.DTOs;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authors.Handlers;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public GetAuthorByIdHandler(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);
        if (author == null)
            return null;
        return _mapper.Map<AuthorDto>(author);
    }
}

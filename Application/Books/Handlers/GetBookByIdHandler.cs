using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Books.Queries;
using Application.Books.DTOs;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Handlers;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBookByIdHandler(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.Id);
        if (book == null)
            return null;
        return _mapper.Map<BookDto>(book);
    }
}

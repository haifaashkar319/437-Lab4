using Core.Domain.Entities;
using Core.Domain.Interfaces;
using MediatR;
using Application.Books.Queries;
using Application.Books.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books.Handlers
{
    public class GetBooksHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetBooksHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            // var all is IEnumerable<Book>, no cast needed
            var all = await _repository.GetAllWithAuthorAsync();

            // 2) filter on the string property
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                all = all.Where(b =>
                    b.Title.Value.ToLower().Contains(search) ||
                    b.Author.Name.Value.ToLower().Contains(search));
            }

            // 3) map the filtered domain list into DTOs
            return _mapper.Map<IEnumerable<BookDto>>(all);
        }

    }
}

using Core.Domain.Interfaces;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Application.Borrowers.DTOs;
using Application.Borrowers.Queries;

namespace Application.Borrowers.Handlers
{
    public class GetBorrowerByIdHandler : IRequestHandler<GetBorrowerByIdQuery, BorrowerDto?>
    {
        private readonly IBorrowerRepository _repository;
        private readonly IMapper _mapper;

        public GetBorrowerByIdHandler(IBorrowerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BorrowerDto?> Handle(GetBorrowerByIdQuery request, CancellationToken cancellationToken)
        {
            var borrower = await _repository.GetByIdAsync(request.Id);
            if (borrower == null)
                return null;
            return _mapper.Map<BorrowerDto>(borrower);
        }
    }
}

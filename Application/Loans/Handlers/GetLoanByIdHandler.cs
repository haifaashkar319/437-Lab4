using Application.Loans.DTOs;
using Application.Loans.Queries;
using Core.Domain.Interfaces;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Loans.Handlers
{
    public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, LoanDto?>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoanByIdHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(request.Id);
            if (loan == null)
                return null;
            return _mapper.Map<LoanDto>(loan);
        }
    }
}

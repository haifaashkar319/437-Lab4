using Application.Loans.DTOs;
using Application.Loans.Queries;
using Core.Domain.Interfaces;
using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Loans.Handlers
{
    public class GetLoansHandler : IRequestHandler<GetLoansQuery, IEnumerable<LoanDto>>
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public GetLoansHandler(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                // Adjust filtering as needed; for example filtering on LoanId as a string:
                all = all.Where(l => l.LoanId.ToString().Contains(search));
            }

            return _mapper.Map<IEnumerable<LoanDto>>(all);
        }
    }
}

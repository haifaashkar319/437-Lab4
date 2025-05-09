using Core.Domain.Entities;
using MediatR;

namespace Application.Loans.Queries;

public class GetLoanByIdQuery : IRequest<Loan?>
{
    public int Id { get; }

    public GetLoanByIdQuery(int id)
    {
        Id = id;
    }
}

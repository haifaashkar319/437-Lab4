using Application.Loans.DTOs;
using MediatR;

namespace Application.Loans.Queries;

public class GetLoanByIdQuery : IRequest<LoanDto?>
{
    public int Id { get; }
    public GetLoanByIdQuery(int id) => Id = id;
}

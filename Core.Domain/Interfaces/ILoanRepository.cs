using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllWithIncludesAsync();
    Task<Loan?> GetByIdAsync(int id);
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task DeleteAsync(Loan loan);
}

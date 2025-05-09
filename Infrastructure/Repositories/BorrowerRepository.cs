using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BorrowerRepository : IBorrowerRepository
{
    private readonly CleanLibraryContext _context;

    public BorrowerRepository(CleanLibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Borrower>> GetAllAsync()
    {
        return await _context.Borrowers.ToListAsync();
    }

    public async Task<Borrower?> GetByIdAsync(int id)
    {
        return await _context.Borrowers.FindAsync(id);
    }

    public async Task AddAsync(Borrower borrower)
    {
        _context.Borrowers.Add(borrower);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Borrower borrower)
    {
        _context.Borrowers.Update(borrower);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Borrower borrower)
    {
        _context.Borrowers.Remove(borrower);
        await _context.SaveChangesAsync();
    }
}

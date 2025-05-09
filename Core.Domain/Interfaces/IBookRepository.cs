using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IBookRepository : IRepository<Book> { 
    Task<IEnumerable<Book>> GetAllWithAuthorAsync();
    Task<Book?> GetByIdWithAuthorAsync(int id);
    Task<Book?> GetByIdAsync(int id);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);

}

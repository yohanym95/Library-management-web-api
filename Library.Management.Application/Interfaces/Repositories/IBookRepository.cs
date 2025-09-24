using Library.Management.Domain.Entities;

namespace Library.Management.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task RemoveAsync(Book book);
}
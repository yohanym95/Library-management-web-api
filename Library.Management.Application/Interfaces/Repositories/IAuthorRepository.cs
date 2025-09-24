using Library.Management.Domain.Entities;

namespace Library.Management.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(int id);
    Task<IEnumerable<Author>> GetAllAsync();
    public Task<Author> GetWithBooksAsync(int id);
    Task AddAsync(Author book);
    Task UpdateAsync(Author book);
    Task RemoveAsync(Author book);
}
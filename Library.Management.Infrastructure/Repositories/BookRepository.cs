using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Domain.Entities;
using Library.Management.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task AddAsync(Book book) => await _context.AddAsync(book);
    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();
    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);
    public async Task RemoveAsync(Book book) => _context.Books.Remove(book);
    public async Task UpdateAsync(Book book) => _context.Books.Update(book);
}
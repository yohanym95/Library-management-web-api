using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Domain.Entities;
using Library.Management.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task AddAsync(Author author) => await _context.AddAsync(author);
    public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.ToListAsync();
    
    public async Task<Author> GetWithBooksAsync(int id) => await _context.Authors
        .Include(a => a.Books)  
        .FirstOrDefaultAsync(a => a.Id == id); 
    public async Task<Author?> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);
    public async Task RemoveAsync(Author author) => _context.Authors.Remove(author);
    public async Task UpdateAsync(Author author) => _context.Authors.Update(author);
}
using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Infrastructure.DBContext;

namespace Library.Management.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    private IBookRepository _bookRepository;
    private IAuthorRepository _authorRepository;

    public UnitOfWork(LibraryDbContext libraryDbContext)
    {
        _context = libraryDbContext;
    }

    public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
    
    public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}
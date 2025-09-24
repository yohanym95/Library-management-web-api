namespace Library.Management.Application.Interfaces.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IBookRepository Books { get;  }
    IAuthorRepository Authors { get;  }
    Task<int> SaveChangesAsync();
}
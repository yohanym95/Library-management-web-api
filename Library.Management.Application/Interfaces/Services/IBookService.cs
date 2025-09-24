using Library.Management.Application.DTOs;

namespace Library.Management.Application.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> AddAsync(CreateBookDto bookDto);
    Task UpdateAsync(int id, UpdateBookDto bookDto);
    Task DeleteAsync(int id);
}
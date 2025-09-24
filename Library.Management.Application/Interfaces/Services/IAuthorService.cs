using Library.Management.Application.DTOs;

namespace Library.Management.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto?> GetByIdAsync(int id);
    Task<AuthorBookDto> GetAllWithBooks(int id);
    Task<AuthorDto> AddAsync(AuthorDto bookDto);
    Task UpdateAsync(int id, AuthorDto bookDto);
    Task DeleteAsync(int id);
}
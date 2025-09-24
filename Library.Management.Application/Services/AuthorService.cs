using AutoMapper;
using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Application.Interfaces.Services;
using Library.Management.Domain.Entities;

namespace Library.Management.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        var authors = await _unitOfWork.Authors.GetAllAsync();
        
        var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
        
        return authorDtos;
    }
    
    public async Task<AuthorBookDto> GetAllWithBooks(int id)
    {
        var authors = await _unitOfWork.Authors.GetWithBooksAsync(id);
        
        var authorDtos = _mapper.Map<AuthorBookDto>(authors);
        
        return authorDtos;
    }

    public async Task<AuthorDto?> GetByIdAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);

        var authorDto = _mapper.Map<AuthorDto?>(author);
        
        return authorDto;
    }

    public async Task<AuthorDto> AddAsync(AuthorDto authorDto)
    {
        var authorEntity = _mapper.Map<Author>(authorDto);

        await _unitOfWork.Authors.AddAsync(authorEntity);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AuthorDto>(authorEntity);
    }

    public async Task UpdateAsync(int id, AuthorDto authorDto)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);

        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }

        author.Name = authorDto.Name;

        await _unitOfWork.Authors.UpdateAsync(author);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);
        
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }

        await _unitOfWork.Authors.RemoveAsync(author);
        
        await _unitOfWork.SaveChangesAsync();
    }
}
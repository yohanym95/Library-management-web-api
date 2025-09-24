using AutoMapper;
using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Application.Interfaces.Services;
using Library.Management.Domain.Entities;

namespace Library.Management.Application.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _unitOfWork.Books.GetAllAsync();
        
        var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
        
        return bookDtos;
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);

        var bookDto = _mapper.Map<BookDto?>(book);
        
        return bookDto;
    }

    public async Task<BookDto> AddAsync(CreateBookDto bookDto)
    {
        var bookEntity = _mapper.Map<Book>(bookDto);

        await _unitOfWork.Books.AddAsync(bookEntity);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BookDto>(bookEntity);
    }

    public async Task UpdateAsync(int id, UpdateBookDto bookDto)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found.");
        }
        
        book.UpdateDetails(bookDto.Title, bookDto.ISBN);

        await _unitOfWork.Books.UpdateAsync(book);

        await _unitOfWork.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);
        
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found.");
        }

        await _unitOfWork.Books.RemoveAsync(book);
        
        await _unitOfWork.SaveChangesAsync();
    }
}
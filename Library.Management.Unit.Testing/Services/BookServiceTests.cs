using AutoMapper;
using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Application.Services;
using Library.Management.Domain.Entities;
using Moq;

namespace Library.Management.Unit.Testing.Services;

public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.Setup(u => u.Books).Returns(_mockBookRepository.Object);

            _bookService = new BookService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", ISBN = "123", AuthorId = 1, CreatedAt = DateTime.UtcNow, IsAvailable = true}
            };
            
            var bookDtos = new List<BookDto>
            {
                new BookDto { Id = 1, Title = "Book1", ISBN = "123",IsAvailable = true, AuthorName = "Yohan"}
            };

            _mockBookRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(books);
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);
            
            var result = await _bookService.GetAllAsync();


            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Book1", result.First().Title);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto_WhenBookExists()
        {
            var book = new Book { Id = 1, Title = "Book1", ISBN = "123", AuthorId = 1,  CreatedAt = DateTime.UtcNow, IsAvailable = true };
            var bookDto = new BookDto { Id = 1, Title = "Book1", ISBN = "123", IsAvailable = true, AuthorName = "Yohan" };

            _mockBookRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            _mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(bookDto);
            
            var result = await _bookService.GetByIdAsync(1);
            
            Assert.NotNull(result);
            Assert.Equal("Book1", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenBookNotFound()
        {
            _mockBookRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);
            _mockMapper.Setup(m => m.Map<BookDto?>(null)).Returns((BookDto?)null);
            
            var result = await _bookService.GetByIdAsync(99);
            
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_AddsBookAndReturnsDto()
        {
            var createBookDto = new CreateBookDto { Title = "New Book", ISBN = "999", AuthorId = 2};
            var book = new Book { Id = 1, Title = "New Book", ISBN = "999", AuthorId = 2 };
            var bookDto = new BookDto { Id = 1, Title = "New Book", ISBN = "999" };

            _mockMapper.Setup(m => m.Map<Book>(createBookDto)).Returns(book);
            _mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(bookDto);
            
            var result = await _bookService.AddAsync(createBookDto);
            
            _mockBookRepository.Verify(r => r.AddAsync(book), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
            
            Assert.Equal("New Book", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesBook_WhenBookExists()
        {
            var updateDto = new UpdateBookDto { Title = "Updated", ISBN = "456", IsAvailable = true};
            var book = new Book { Id = 1, Title = "Old Title", ISBN = "123", AuthorId = 1, IsAvailable = true, CreatedAt = DateTime.UtcNow};

            _mockBookRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            
            await _bookService.UpdateAsync(1, updateDto);
            
            Assert.Equal("Updated", book.Title);
            Assert.Equal("456", book.ISBN);
            
            _mockBookRepository.Verify(r => r.UpdateAsync(book), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Throws_WhenBookNotFound()
        {
            var updateDto = new UpdateBookDto { Title = "Updated", ISBN = "456" };
            _mockBookRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.UpdateAsync(99, updateDto));
        }

        [Fact]
        public async Task DeleteAsync_RemovesBook_WhenBookExists()
        {
            
            var book = new Book { Id = 1, Title = "To Delete", ISBN = "321", AuthorId = 1 };
            _mockBookRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
            
            await _bookService.DeleteAsync(1);
            
            _mockBookRepository.Verify(r => r.RemoveAsync(book), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Throws_WhenBookNotFound()
        {
            _mockBookRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.DeleteAsync(99));
        }
    }
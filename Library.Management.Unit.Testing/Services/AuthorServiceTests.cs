using AutoMapper;
using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Repositories;
using Library.Management.Application.Services;
using Library.Management.Domain.Entities;
using Moq;

namespace Library.Management.Unit.Testing.Services;

public class AuthorServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.Setup(u => u.Authors).Returns(_mockAuthorRepository.Object);

            _authorService = new AuthorService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            var authors = new List<Author> { new Author { Id = 1, Name = "Author1" } };
            var authorDtos = new List<AuthorDto> { new AuthorDto {  Name = "Author1" } };

            _mockAuthorRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(authors);
            _mockMapper.Setup(m => m.Map<IEnumerable<AuthorDto>>(authors)).Returns(authorDtos);
            
            var result = await _authorService.GetAllAsync();
            
            Assert.Single(result);
            Assert.Equal("Author1", result.First().Name);
        }

        [Fact]
        public async Task GetAllWithBooks_ReturnsMappedDto()
        {
            var author = new Author
            {
                Id = 1,
                Name = "Author1"
            };

            var authorBookDto = new AuthorBookDto
            {
                Id = 1,
                Name = "Author1",
                Books = new List<Book> { new Book() { Id = 10, Title = "Book1", ISBN = "123" } }
            };

            _mockAuthorRepository.Setup(r => r.GetWithBooksAsync(1)).ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map<AuthorBookDto>(author)).Returns(authorBookDto);
            
            var result = await _authorService.GetAllWithBooks(1);
            
            Assert.NotNull(result);
            Assert.Equal("Author1", result.Name);
            Assert.Single(result.Books);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto_WhenAuthorExists()
        {
            var author = new Author { Id = 1, Name = "Author1" };
            var authorDto = new AuthorDto { Name = "Author1" };

            _mockAuthorRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);
            
            var result = await _authorService.GetByIdAsync(1);
            
            Assert.NotNull(result);
            Assert.Equal("Author1", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            _mockAuthorRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author?)null);
            _mockMapper.Setup(m => m.Map<AuthorDto?>(null)).Returns((AuthorDto?)null);
            
            var result = await _authorService.GetByIdAsync(99);
            
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_AddsAuthorAndReturnsDto()
        {
            var authorDto = new AuthorDto { Name = "New Author" };
            var authorEntity = new Author { Id = 1, Name = "New Author" };
            var savedDto = new AuthorDto {  Name = "New Author" };

            _mockMapper.Setup(m => m.Map<Author>(authorDto)).Returns(authorEntity);
            _mockMapper.Setup(m => m.Map<AuthorDto>(authorEntity)).Returns(savedDto);
            
            var result = await _authorService.AddAsync(authorDto);
            
            _mockAuthorRepository.Verify(r => r.AddAsync(authorEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal("New Author", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesAuthor_WhenFound()
        {
            var author = new Author { Id = 1, Name = "Old Name" };
            var updateDto = new AuthorDto { Name = "Updated Name" };

            _mockAuthorRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);
            
            await _authorService.UpdateAsync(1, updateDto);
            
            Assert.Equal("Updated Name", author.Name);
            _mockAuthorRepository.Verify(r => r.UpdateAsync(author), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Throws_WhenNotFound()
        {
            var updateDto = new AuthorDto { Name = "DoesNotExist" };
            _mockAuthorRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author?)null);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _authorService.UpdateAsync(99, updateDto));
        }
        
        [Fact]
        public async Task DeleteAsync_RemovesAuthor_WhenFound()
        {
            var author = new Author { Id = 1, Name = "To Delete" };
            _mockAuthorRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);

            await _authorService.DeleteAsync(1);

            _mockAuthorRepository.Verify(r => r.RemoveAsync(author), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Throws_WhenNotFound()
        {
            _mockAuthorRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author?)null);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _authorService.DeleteAsync(99));
        }
    }

namespace Library.Management.Domain.Entities;

public class Author
{
    public int Id { get;  set; }
    public string Name { get;  set; } = string.Empty;
    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();
    
}
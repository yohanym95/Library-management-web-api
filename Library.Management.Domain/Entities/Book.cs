namespace Library.Management.Domain.Entities;

public class Book
{
    public int Id { get;  set; }
    public string Title { get;  set; } = string.Empty;
    public string? ISBN { get;  set; }
    public int AuthorId { get;  set; }
    public bool IsAvailable { get;  set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    public void UpdateDetails(string title, string? isbn)
    {
        Title = title;
        ISBN = isbn;
    }
}
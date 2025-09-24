namespace Library.Management.Application.DTOs;

public class BookDto
{
    //public BookDto() { }
    
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ISBN { get; set; }
    public string? AuthorName { get; set; }
    public bool IsAvailable { get; set; }
}
namespace Library.Management.Application.DTOs;

public class CreateBookDto
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string? ISBN { get; set; }
}
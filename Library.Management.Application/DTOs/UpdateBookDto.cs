namespace Library.Management.Application.DTOs;
public class UpdateBookDto
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }
    public string? ISBN { get; set; }
}
using Library.Management.Domain.Entities;

namespace Library.Management.Application.DTOs;

public class AuthorBookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}
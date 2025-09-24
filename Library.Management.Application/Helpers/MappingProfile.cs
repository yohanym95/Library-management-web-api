using AutoMapper;
using Library.Management.Application.DTOs;
using Library.Management.Domain.Entities;

namespace Library.Management.Application.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain → DTO
        CreateMap<Book, BookDto>().ReverseMap();
        
        CreateMap<CreateBookDto, Book>().ReverseMap();

        CreateMap<AuthorDto, Author>().ReverseMap();
        CreateMap<AuthorBookDto, Author>().ReverseMap();
    }
}
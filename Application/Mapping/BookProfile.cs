using Application.Books.DTOs;
using Core.Domain.Entities;  // assuming Book entity lives here
using AutoMapper;

namespace Application.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        // Domain → DTO
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        // DTO → Domain
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        CreateMap<UpdateBookDto, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
    }
}

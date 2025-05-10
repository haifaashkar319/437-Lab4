using Application.Authors.DTOs;
using Application.Books.DTOs;
using Application.Borrowers.DTOs;
using AutoMapper;
using Core.Domain.Entities;
using LibraryManagement.ViewModels;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<Author, AuthorListViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));
        CreateMap<AuthorDto, AuthorListViewModel>()
           .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<AuthorDto, AuthorDetailViewModel>()
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

        CreateMap<AuthorCreateViewModel, CreateAuthorDto>();
        CreateMap<AuthorEditViewModel, UpdateAuthorDto>();

        CreateMap<Book, BookListViewModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        CreateMap<BookDto, BookListViewModel>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        CreateMap<BookDto, BookDetailViewModel>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        CreateMap<BookCreateViewModel, CreateBookDto>();
        CreateMap<BookEditViewModel, UpdateBookDto>();

        // New Borrower mappings
        CreateMap<Borrower, BorrowerListViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<BorrowerDto, BorrowerListViewModel>()
            .ForMember(dest => dest.BorrowerId, opt => opt.MapFrom(src => src.BorrowerId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<BorrowerDto, BorrowerDetailViewModel>()
            .ForMember(dest => dest.BorrowerId, opt => opt.MapFrom(src => src.BorrowerId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<BorrowerCreateViewModel, CreateBorrowerDto>();
        CreateMap<BorrowerEditViewModel, UpdateBorrowerDto>();
    }
}

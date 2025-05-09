using Application.Authors.DTOs;
using Application.Books.DTOs;
using Application.Borrowers.DTOs;
using Application.Loans.DTOs;
using AutoMapper;
using Core.Domain.Entities;
using Presentation.ViewModels;

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

        // Updated Loan mappings using LoanListViewModel.cs
        CreateMap<Loan, LoanListViewModel>()
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.Borrower.Name))
            .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.LoanDate.AddDays(14)));

        CreateMap<LoanDto, LoanListViewModel>()
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.BookTitle, opt => opt.Ignore())   // BookTitle should come from navigation; fill manually if needed
            .ForMember(dest => dest.BorrowerName, opt => opt.Ignore()) // BorrowerName should come from navigation; fill manually if needed
            .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.LoanDate.AddDays(14)));

        CreateMap<LoanDto, LoanDetailViewModel>()
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.BorrowerId))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.ReturnDate));

        CreateMap<LoanCreateViewModel, CreateLoanDto>();
        CreateMap<LoanEditViewModel, UpdateLoanDto>();
    }
}

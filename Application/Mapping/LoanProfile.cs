using Application.Loans.DTOs;
using Core.Domain.Entities;
using AutoMapper;

namespace Application.Mapping
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            // 1) Domain → DTO
            CreateMap<Loan, LoanDto>()
                .ForMember(d => d.BookTitle,
                           o => o.MapFrom(s => s.Book.Title.Value))
                .ForMember(d => d.BorrowerName,
                           o => o.MapFrom(s => s.Borrower.Name.Value))
                .ForMember(d => d.LoanId,
                           o => o.MapFrom(s => s.LoanId))
                .ForMember(d => d.BookId,
                           o => o.MapFrom(s => s.BookId))
                .ForMember(d => d.BorrowerId,
                           o => o.MapFrom(s => s.BorrowerId))
                .ForMember(d => d.LoanDate,
                           o => o.MapFrom(s => s.LoanDate))
                .ForMember(d => d.ReturnDate,
                           o => o.MapFrom(s => s.DueDate));

            // 2) CreateLoanDto → Loan
            //    Ignores LoanId and ReturnDate (as these are handled by the database or business logic)
            CreateMap<CreateLoanDto, Loan>()
                .ForMember(dest => dest.LoanId, opt => opt.Ignore())
                .ForMember(dest => dest.DueDate, opt => opt.Ignore());

            // 3) UpdateLoanDto → Loan
            //    Maps all fields by convention, allowing update of all properties
            CreateMap<UpdateLoanDto, Loan>();
        }
    }
}

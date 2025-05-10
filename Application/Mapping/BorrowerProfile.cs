using Application.Borrowers.DTOs;
using Core.Domain.Entities;      // assuming Borrower entity exists here
using AutoMapper;

namespace Application.Mapping;

public class BorrowerProfile : Profile
{
    public BorrowerProfile()
    {
        // Domain → DTO
        CreateMap<Borrower, BorrowerDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        // DTO → Domain
        CreateMap<CreateBorrowerDto, Borrower>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateBorrowerDto, Borrower>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}

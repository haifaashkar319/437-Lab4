using Application.Authors.DTOs;
using Core.Domain.Entities;      // assuming Author lives here
using Core.Domain.ValueObjects;  // assuming AuthorName lives here
using AutoMapper;                // for AutoMapper Profile and CreateMap
namespace Application.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        // Domain → DTO 
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));

        // DTO → Domain 
        CreateMap<CreateAuthorDto, Author>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new AuthorName(src.Name)));

        CreateMap<UpdateAuthorDto, Author>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new AuthorName(src.Name)));

    }
}

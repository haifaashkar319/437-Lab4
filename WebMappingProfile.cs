using Application.Authors.DTOs;
using AutoMapper;
using Core.Domain.Entities;
using LibraryManagement.ViewModels;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<Author, AuthorListViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));
        // ...repeat for other view models as needed...
        CreateMap<AuthorDto, AuthorListViewModel>()
           .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        // If you have a Details view:
        CreateMap<AuthorDto, AuthorDetailViewModel>()
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

        CreateMap<AuthorCreateViewModel, CreateAuthorDto>();
        CreateMap<AuthorEditViewModel, UpdateAuthorDto>();
    }
}

using AutoMapper;
using Configuration.Shared.Entities;
using Users.Models;
using ViewModels.RazorPages.Configuration;
using ViewModels.RazorPages.Users;

namespace PortalBlazor.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, UserAdminViewModel>().ReverseMap();
        CreateMap<AppUser, UserAdminCreateViewModel>().ReverseMap();
        CreateMap<ApplicationConfiguration, ApplicationConfigurationViewModel>().ReverseMap();
    }
}
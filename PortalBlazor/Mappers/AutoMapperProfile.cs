using AutoMapper;
using Users.Models;
using ViewModels.RazorPages.Users;

namespace PortalBlazor.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, UserAdminViewModel>().ReverseMap();
        CreateMap<AppUser, UserAdminCreateViewModel>().ReverseMap();
    }
}
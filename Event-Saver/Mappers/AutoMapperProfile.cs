using Abstract.Users;
using AutoMapper;
using Event_Saver.Components.Modals;
using Users.Models;
using ViewModels.RazorPages.Users;

namespace Event_Saver.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, UserAdminViewModel>().ReverseMap();
        CreateMap<AppUser, UserAdminCreateViewModel>().ReverseMap();
    }
}
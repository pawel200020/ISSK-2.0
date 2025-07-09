using Abstract.Users;
using AutoMapper;
using Users.Models;
using ViewModels.RazorPages.Users;

namespace Event_Saver.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAdminViewModel, IUser>();
        CreateMap<IUser, UserAdminViewModel>().ReverseMap();
        CreateMap<AppUser, UserAdminViewModel>().ReverseMap();
    }
}
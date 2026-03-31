using AutoMapper;
using Configuration.Shared;
using Configuration.Shared.Entities;
using Configuration.Shared.Notifications;
using Users.Shared.Models;
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
        CreateMap<SmtpConfigurationViewModel, ISmtpConfiguration>().As<SmtpConfiguration>();
        CreateMap<SmtpConfiguration, SmtpConfigurationViewModel>().ReverseMap();
    }
}
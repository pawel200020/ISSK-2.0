using AutoMapper;
using Configuration.Shared;
using Configuration.Shared.Entities;
using Configuration.Shared.Notifications;
using EventsSaver.Shared.Entities;
using Users.Shared.Models;
using ViewModels.RazorPages.Configuration;
using ViewModels.RazorPages.EventSaver.Seasons;
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
        CreateMap<SeasonViewModel,SeasonEntity>().ReverseMap();
        CreateMap<SeasonViewModel, ISeasonEntity>().As<SeasonEntity>();
    }
}
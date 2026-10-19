using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLoginProvider;
using AutoMapper;
using Configuration.Shared.Entities;
using Configuration.Shared.Notifications;
using EventsSaver.Shared.Entities;
using PortalBlazor.Mappers.Mappings.ExternalLogin;
using Users.Shared.Models;
using ViewModels.RazorPages.Configuration;
using ViewModels.RazorPages.EventSaver.Lines;
using ViewModels.RazorPages.EventSaver.Seasons;
using ViewModels.RazorPages.Users;

namespace PortalBlazor.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, UserAdminViewModel>().ReverseMap();
        CreateMap<AppUser, UserAdminCreateViewModel>().ReverseMap();
        CreateMap<IExternalLoginProvider, ExternalLoginProviderConfigurationViewModel>();
        CreateMap<ExternalLoginProviderConfigurationViewModel, IExternalLoginProvider>()
            .ConstructUsing(_ => new ExternalLoginProvider());
        CreateMap<ApplicationConfiguration, ApplicationConfigurationViewModel>().ReverseMap();
        CreateMap<SmtpConfigurationViewModel, ISmtpConfiguration>().As<SmtpConfiguration>();
        CreateMap<SmtpConfiguration, SmtpConfigurationViewModel>().ReverseMap();
        CreateMap<SeasonViewModel,SeasonEntity>().ReverseMap();
        CreateMap<SeasonViewModel, ISeasonEntity>().As<SeasonEntity>();
        CreateMap<LineViewModel, LineEntity>().ReverseMap();
        CreateMap<LineViewModel, ILineEntity>().As<LineEntity>();
        
    }
}
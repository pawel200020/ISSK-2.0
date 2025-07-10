using BlazorBootstrap;

namespace PortalBlazor.Toasts;

public class ToastMessageCreationService
{
    public ToastMessage CreateToastMessage(string title, string message, ToastType toastType)
        => new ToastMessage
        {
            Type = toastType,
            Title = title,
            HelpText = $"{DateTime.Now}",
            Message = message,
        };
    
    public ToastMessage CreateUnknownErrorToastMessage()
        => new ToastMessage
        {
            Type = ToastType.Danger,
            Title = "Error",
            HelpText = $"{DateTime.Now}",
            Message = "failed with unknown exception",
        };
}
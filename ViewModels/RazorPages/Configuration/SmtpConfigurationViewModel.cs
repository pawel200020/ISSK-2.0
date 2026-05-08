namespace ViewModels.RazorPages.Configuration;

public class SmtpConfigurationViewModel
{
    public string? SmtpServerAddress { get; set; }
    public int? SmtpServerPort { get; set; }
    public bool UseSsl { get; set; }
}
using Configuration.Shared.Notifications;
using Newtonsoft.Json;

namespace Configuration.Shared.Entities;

public class SmtpConfiguration : ISmtpConfiguration
{
    [JsonProperty("serverAddress")]
    public string? SmtpServerAddress { get; set; }
    [JsonProperty("serverPort")]
    public int? SmtpServerPort { get; set; }
    [JsonProperty("useSsl")]
    public bool UseSsl { get; set; }
}
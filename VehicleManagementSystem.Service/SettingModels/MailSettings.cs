using System.Reflection.Metadata;

namespace VehicleManagementSystem.Service.SettingModels;

public class MailSettings
{
    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public string FromAddress { get; set; } = string.Empty;
}
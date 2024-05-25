using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleManagementSystem.Service.SettingModels;

namespace Project.Service.Data;

public static class AddEmailService
{
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration
        services.AddOptions<MailSettings>().Bind(config: configuration.GetSection(key: nameof(MailSettings)));
        // Service
        services.AddSingleton<IEmailSender, EmailService>();

        return services;
    }
}
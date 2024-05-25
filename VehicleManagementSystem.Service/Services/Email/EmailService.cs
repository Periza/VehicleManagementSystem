using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using VehicleManagementSystem.Service.SettingModels;

namespace Project.Service.Data;

public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
{

    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async  Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MimeMessage message = new();
        message.From.Add(new MailboxAddress(name: "admin", address: _mailSettings.FromAddress));
        message.To.Add(new MailboxAddress(name: "customer", address: "customer@example.com"));
        message.Subject = subject;

        message.Body = new TextPart(subtype: "html")
        {
            Text = htmlMessage
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(host: _mailSettings.Host, _mailSettings.Port);
            
            // Sent the email
            await client.SendAsync(message: message);
            
            // Disconnect
            await client.DisconnectAsync(quit: true);
        }
    }
}
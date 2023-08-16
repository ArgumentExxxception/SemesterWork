using System;
using Core1.Interfaces;
using HW2Sem.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure1.Services;

public class MailService: IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendWelcomeEmailAsync(WelcomeRequest request)
    {
        string filePath = "/Users/almazahmetsin/Documents/vs-code/HW2Sem/HW2Sem/Template/WelcomeTemplate.html";
        
        StreamReader str = new StreamReader(filePath);
        
        var mailText = await str.ReadToEndAsync();
        
        str.Close();
        
        mailText = mailText.Replace("[username]", request.Username).Replace("[email]",request.ToEmail);
        
        var email = new MimeMessage();
        
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        
        email.To.Add(MailboxAddress.Parse(request.ToEmail));
        
        email.Subject = $"Welcome {request.Username}";
        
        var builder = new BodyBuilder();
        
        builder.HtmlBody = mailText;
        
        email.Body = builder.ToMessageBody();
        
        using var smtp = new SmtpClient();
        
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        
        await smtp.SendAsync(email);
        
        await smtp.DisconnectAsync(true);
        
    }
}
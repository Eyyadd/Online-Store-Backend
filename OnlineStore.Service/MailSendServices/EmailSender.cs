using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineStore.Domain.Utilities;
using OnlineStore.Infrastructure.MailSendServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.MailSendServices
{
    public class EmailSender : ISendEmail
    {
        private readonly IConfiguration emailOptions;

        public EmailSender(IConfiguration  emailOptions)
        {
            this.emailOptions = emailOptions;
        }
        public async Task SendEmailAsync(Email email)
        {
            var Mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailOptions["MailSetting:SenderEmail"]),
                Subject = email.Subject,

            };
            Mail.To.Add(MailboxAddress.Parse(email.To));
            Mail.From.Add(new MailboxAddress(emailOptions["MailSetting:DisplayName"], emailOptions["MailSetting:SenderEmail"]));
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            Mail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(emailOptions["MailSetting:Host"], int.Parse(emailOptions["MailSetting:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailOptions["MailSetting:SenderEmail"], emailOptions["MailSetting:Password"]);
            await smtp.SendAsync(Mail);
            await smtp.DisconnectAsync(true);
            
        }
    }
}

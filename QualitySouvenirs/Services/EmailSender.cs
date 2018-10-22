using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace QualitySouvenirs.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Shivneel Achari", "achars05@myunitec.ac.nz"));
            mes.To.Add(new MailboxAddress("User", email));
            mes.Subject = subject;

            mes.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.office365.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("achars05@myunitec.ac.nz", "18021998");

                client.Send(mes);
                client.Disconnect(true);
            }
            // Plug in your email service here to send an email.
            return Task.CompletedTask;
        }
    }
}
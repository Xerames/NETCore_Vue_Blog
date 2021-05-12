using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class EmailSender : IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        public Task ConfirmationMailAsync(string link, string email)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            var subject = $"www.MySite.com || Confirm Email";
            var body = "<h2>Please click this link for confirm email</h2><hr/>";
            body += $"<a href='{link}'>Confirmation Link</a>";
            return client.SendMailAsync(
            new MailMessage(userName, email, subject, body) { IsBodyHtml = true }
            );
        }

        public Task ContactUsAsync(string fullName, string email, string subject, string message)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            var body = $"<h2>Hello I'm {fullName} </h2> {message}";
            return client.SendMailAsync(
            new MailMessage(email, userName, subject, body) { IsBodyHtml = true }
            );
        }

        public Task ForgetPasswordMailAsync(string link, string email)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            var subject = $"www.MySite.com || Reset Password";
            var body = "<h2>Please click this link for reset password</h2><hr/>";
            body += $"<a href='{link}'>reset password link</a>";
            return client.SendMailAsync(
            new MailMessage(userName, email, subject, body) { IsBodyHtml = true }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public interface IEmailSender
    {
        public Task ConfirmationMailAsync(string link, string email);
        public Task ForgetPasswordMailAsync(string link, string email);
        public Task ContactUsAsync(string fullName, string email, string subject, string message);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSender
    {
        string SendEmail(EmailMessage message);
        Task<string> SendEmailForContactAsync(EmailMessage message);
    }
}

using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using Core;
using MailKit.Net.Smtp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        private readonly string _messageSended = "Sended";
        private readonly string _tagName = "<replaceable>name<replaceable />";
        private readonly string _tagAddress = "<replaceable>address<replaceable />";
        private readonly string _tagSubject = "<replaceable>subject<replaceable />";
        private readonly string _tagContent = "<replaceable>content<replaceable />";
        private readonly string _templateRelativePath = "../EmailService/EmailTemplates/";
        private readonly string _templateContactFile = "contact.html";

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public string SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            return Send(emailMessage);
        }

        public async Task<string> SendEmailForContactAsync(EmailMessage message)
        {
            string resultMessage;
            try
            {
                message.Content = GenerateContentForContact(message);
                
                message.From = new Dictionary<string, string>()
                {
                    { _emailConfig.SenderAddress, _emailConfig.SenderName }
                };
                message.To = new Dictionary<string, string>()
                {
                    { _emailConfig.ContactRecipientAddress, _emailConfig.ContactRecipientName }
                };

                var emailMessage = CreateEmailMessage(message);
                resultMessage = await SendAsync(emailMessage);
            } 
            catch(Exception e)
            {
                resultMessage = Utils.GetDeepException(e).ToString();
            }

            return resultMessage;
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.AddRange(message.GetFromAsMailboxAddress());
            emailMessage.To.AddRange(message.GetToAsMailboxAddress());
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        private string Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                var message = "";
                
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove(
                        SystemParameters.EmailService.AuthenticationMechanism);
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);

                    message = _messageSended;
                }
                catch(Exception ex)
                {
                    message = Utils.GetDeepException(ex).ToString();
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

                return message;
            }
        }

        private async Task<string> SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                var message = "";

                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove(
                        SystemParameters.EmailService.AuthenticationMechanism);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);

                    message = _messageSended;
                }
                catch (Exception ex)
                {
                    message = Utils.GetDeepException(ex).ToString();
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }

                return message;
            }
        }

        private string GenerateContentForContact(EmailMessage message)
        {
            var content = string.Empty;

            try
            {
                if (message == null || message.From == null)
                    return content;

                var template = string.Empty;
                var fullPath = Path.GetFullPath(_templateRelativePath + _templateContactFile);
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    template = reader.ReadToEnd();
                }

                message.Content = message.Content.Replace("\n", "<br/>");
                template = template.Replace(_tagName, message.From.FirstOrDefault().Value);
                template = template.Replace(_tagAddress, message.From.FirstOrDefault().Key);
                template = template.Replace(_tagSubject, message.Subject);
                template = template.Replace(_tagContent, message.Content);

                content = template;
            }
            catch(Exception e)
            {

            }

            return content;
        }
    }
}

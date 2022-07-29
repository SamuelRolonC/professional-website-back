using Core;
using Core.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        private readonly string _messageSent = "0";
        
        private readonly string _tagReplaceable = "<replaceable>X<replaceable />";

        private readonly string _replaceName = "name";
        private readonly string _replaceAddress = "address";
        private readonly string _replaceSubject = "subject";
        private readonly string _replaceContent = "content";

        private readonly string _templateRelativePath = "../EmailService/EmailTemplates/";
        private readonly string _templateContactFile = "Contact/contact.es.html";

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
                message.Subject = $"{SystemParameters.General.WebsiteName} - Tenés un nuevo mensaje";
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

        #region Private functions

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

                    message = _messageSent;
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

                    message = _messageSent;
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
                template = template.Replace(GetReplaceableTag(_replaceName), message.From.FirstOrDefault().Value);
                template = template.Replace(GetReplaceableTag(_replaceAddress), message.From.FirstOrDefault().Key);
                template = template.Replace(GetReplaceableTag(_replaceSubject), message.Subject);
                template = template.Replace(GetReplaceableTag(_replaceContent), message.Content);

                content = template;
            }
            catch(Exception e)
            {

            }

            return content;
        }

        private string GetReplaceableTag(string replaceable)
        {
            var replaceableTag = _tagReplaceable.Replace("X", replaceable);
            return replaceableTag;
        }

        #endregion
    }
}

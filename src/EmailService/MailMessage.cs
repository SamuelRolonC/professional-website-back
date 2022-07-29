using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailMessage
    {
        public Dictionary<string, string> From { get; set; }
        public Dictionary<string, string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        
        public EmailMessage()
        {

        }

        public EmailMessage(string fromName, string fromAddress, string subject, string content)
        {
            From = new Dictionary<string, string>
            {
                { fromAddress, fromName }
            };
            Subject = subject;
            Content = content;
        }

        public EmailMessage(string fromName, string fromAddress, string toName, string toAddress, string subject
            , string content)
        {
            From = new Dictionary<string, string>
            {
                { fromAddress, fromName }
            };
            To = new Dictionary<string, string>
            {
                { toAddress, toName }
            };
            Subject = subject;
            Content = content;
        }

        public void ValidateForContactWithThrow()
        {
            var errors = new StringBuilder();

            if (From == null || string.IsNullOrEmpty(From.FirstOrDefault().Key))
                errors.AppendLine("No se ingresó la dirección de correo.");
            
            if (From == null || string.IsNullOrEmpty(From.FirstOrDefault().Value))
                errors.AppendLine("No se ingresó el nombre.");
            
            if (string.IsNullOrEmpty(Subject))
                errors.AppendLine("No se ingresó un asunto.");

            if (string.IsNullOrEmpty(Content))
                errors.AppendLine("El mensaje está vacío.");

            if (errors.Length == 0)
                throw new Exception(errors.ToString());
        }

        private List<MailboxAddress> GetDictionaryAsMailboxAddress(Dictionary<string, string> pairs)
        {
            var listMailboxAddress = new List<MailboxAddress>();

            foreach(var pair in pairs)
            {
                listMailboxAddress.Add(new MailboxAddress(pair.Value, pair.Key));
            }

            return listMailboxAddress;
        }

        public List<MailboxAddress> GetFromAsMailboxAddress()
        {
            return GetDictionaryAsMailboxAddress(From);
        }

        public List<MailboxAddress> GetToAsMailboxAddress()
        {
            return GetDictionaryAsMailboxAddress(To);
        }
    }
}

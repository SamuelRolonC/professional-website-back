using System;

namespace EmailService
{
    public class EmailConfiguration
    {
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ContactRecipientName { get; set; }
        public string ContactRecipientAddress { get; set; }
    }
}

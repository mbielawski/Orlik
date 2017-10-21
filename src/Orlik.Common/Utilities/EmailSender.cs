using System.Net.Mail;
using Orlik.Common.Configuration;

namespace Orlik.Common.Utilities
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(MailMessage message)
        {
            var client = SmtpConfig.Instance.CreateClient();
            client.Send(message);
        }
    }
}

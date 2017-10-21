using System.Net.Mail;

namespace Orlik.Common.Utilities
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
    }
}

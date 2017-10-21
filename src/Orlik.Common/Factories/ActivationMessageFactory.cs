using System;
using System.Net.Mail;

namespace Orlik.Common.Factories
{
    public static class ActivateMessageFactory
    {
        private static readonly string _addressFrom = "AplikacjaOrlik@gmail.com";
        private static readonly string _subject = "Aktywacja konta";

        public static MailMessage CreateActivationMessage(string address, Guid activationId)
        {
            var message = new MailMessage
            {
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(address));
            message.From = new MailAddress(_addressFrom);
            message.Subject = _subject;
            var link =
                $"<a href=\"http://localhost:20371/Users/Activate/?activationId={activationId}\">tutaj</a> ";
            message.Body = $"Dziękujemy za rejestrację! Kliknij {link} aby aktywować konto!";

            return message;
        }
    }
}


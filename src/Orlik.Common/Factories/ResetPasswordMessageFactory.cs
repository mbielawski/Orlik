using System;
using System.Net.Mail;

namespace Orlik.Common.Factories
{
    public static class ResetPasswordMessageFactory
    {
        private static readonly string _addressFrom = "AplikacjaOrlik@gmail.com";
        private static readonly string _subject = "Resetowanie hasła";

        public static MailMessage CreateResetPasswordMessage(string address, Guid token)
        {
            var message = new MailMessage
            {
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(address));
            message.From = new MailAddress(_addressFrom);
            message.Subject = _subject;
            var link =
                $"<a href=\"http://localhost:20371/Account/ResetPasswordConfirmed/?token={token}\">tutaj</a> ";
            message.Body = $"W celu zresetowania hasła kliknij {link}.";

            return message;
        }
    }
}

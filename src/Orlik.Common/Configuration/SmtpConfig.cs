using System.Net;
using System.Net.Mail;

namespace Orlik.Common.Configuration
{
    public class SmtpConfig
    {
        private static SmtpConfig _instance;

        public static SmtpConfig Instance
            => _instance ?? (_instance = new SmtpConfig());

        private SmtpConfig()
        {
        }

        public SmtpClient CreateClient()
        {
            var client = new SmtpClient();
            var credential = new NetworkCredential
            {
                UserName = "AplikacjaOrlik@gmail.com",
                Password = "" //Removed
            };

            client.Credentials = credential;

            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            return client;
        }
    }
}

using backend.Services.Abstract;
using System.Net;
using System.Net.Mail;

namespace backend.Services.Concrete
{
    public class EmailerManager : IEmailerService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailerManager(IConfiguration configuration)
        {
            _smtpPort = Convert.ToInt32(configuration["Smtp:Port"]!);
            _smtpServer = configuration["Smtp:Server"]!;
            _smtpUsername = configuration["Smtp:Username"]!;
            _smtpPassword = configuration["Smtp:Password"]!;
        }
        public async Task SendEmailVerificationToken(string recipient, string emailToken)
        {
            MailAddress to = new MailAddress(recipient);
            MailAddress from = new MailAddress(_smtpUsername);

            MailMessage email = new MailMessage(from, to);

            email.Subject = "Verify your account";
            email.Body = $"https://localhost:7130/api/Auth/Verify?token={emailToken}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = _smtpServer;
            smtpClient.Port = _smtpPort;
            smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await smtpClient.SendMailAsync(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}

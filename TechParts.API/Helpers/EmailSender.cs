

using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace TechParts.API.Helpers
{
    public class EmailSender
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        public static void SendEmail(string receiver, string subject, string body, string receiverName)
        {
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress(_config.GetValue<string>("EmailConfig:Name"), _config.GetValue<string>("EmailConfig:Sender"));
            message.Reciever = new MailboxAddress(receiverName, receiver);
            message.Subject = subject;
            message.Content = body;
            var mimeMessage = EmailSender.CreateMailMessage(message);

           try
           {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Connect(_config.GetValue<string>("EmailConfig:Host"), _config.GetValue<int>("EmailConfig:Port"));
                    smtpClient.Authenticate(_config.GetValue<string>("EmailConfig:Username"), _config.GetValue<string>("EmailConfig:Password"));
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                }
                System.Console.WriteLine("Mail sent!");
           }
           catch(System.Exception e)
           {
               System.Console.WriteLine(e.Message);
           }
        }

        private static MimeMessage CreateMailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return mimeMessage;
        }
    }
}
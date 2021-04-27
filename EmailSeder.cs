using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace email_sender_demo
{
    public interface IEmailSenderService
    {
        void Send(string fromEmail, string toEmail, string subject, string bodyHtml);
    }
    public class EmailSenderService : IEmailSenderService
    {       
        private readonly string SMTP_USER = Environment.GetEnvironmentVariable("SMTP_USER");
        private readonly string SMTP_PASS = Environment.GetEnvironmentVariable("SMTP_PASS");

        public void Send(string fromEmail, string toEmail, string subject, string bodyHtml)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = bodyHtml };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(SMTP_USER, SMTP_PASS);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

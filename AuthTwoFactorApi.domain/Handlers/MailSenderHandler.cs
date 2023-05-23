using AuthTwoFactorApi.Domain.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using static QRCoder.PayloadGenerator;

namespace AuthTwoFactorApi.Domain.Handlers
{
    public class MailSenderHandler : IMailSenderHandler
    {
        public MailSenderHandler() { }

        public void SendMail(string acessCode, string mail)
        {

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender Name", "sender@mail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", mail));

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<html>" +
                    "<body>" +
                    "<h1>Auth code</h1>" +
                    "<p>This is your confirmation code, please enter it in the verification field!</p>" +
                    $"<h1>{acessCode}</h1>" +
                    "</body>" +
                    "</html>"
            };


            var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, false);
            smtp.Authenticate("al.rutherford@ethereal.email", "YzDWedYVv4AnrGfKGB");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

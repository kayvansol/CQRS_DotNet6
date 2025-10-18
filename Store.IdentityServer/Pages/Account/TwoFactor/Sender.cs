using System.Net;
using System.Net.Mail;

namespace Store.IdentityServer.Pages.Account.TwoFactor
{
    public class Sender
    {

        private Setting _setting;

        public Sender(Setting setting)
        {
            _setting = setting;
        }


        public async Task Send(MailMessage mail)
        {
            using (SmtpClient smtpClient = new SmtpClient(_setting.SmtpServer, _setting.SmtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_setting.SenderAddress, _setting.Password);
                smtpClient.Timeout = 10000;
                await smtpClient.SendMailAsync(mail);
            }

        }
    }
}

using Microsoft.Extensions.Options;
using NewsApp.CORE.OptionModels;
using NewsApp.SERVICE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendResetPasswordEmail(string resetEmailLink,string toEmail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;


            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "DailNews Şifre Sıfırlama Linki";
            mailMessage.Body = $"<h3>Bu şifre sıfırlama linki Yazılım Mühendisliği Dersi, Gazete Uygulaması Proje Ödevi Uygulaması tarafından gönderilmektedir. Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h3>" +
                $"<p><a href=`{resetEmailLink}`> Şifre Yenileme Link</a></p>";


            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}

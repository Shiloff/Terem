using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Project.WebUI.Services
{
    public class YandexEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            string Account = "racerbugaga3@gmail.com";//ConfigurationManager.AppSettings["emailService:Account"];
            string Password = "St985kxb0";//ConfigurationManager.AppSettings["emailService:Password"];


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(Account);
                mail.To.Add(new MailAddress(message.Destination));
                mail.Subject = message.Subject;                
                mail.Body =  message.Body;
                mail.IsBodyHtml = true;
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["emailService:Host"];
                    client.Port = 587;//Convert.ToInt32(ConfigurationManager.AppSettings["emailService:Port"]);
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("racerbugaga3@gmail.com", "mniqrszmsxoyygvm");
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await client.SendMailAsync(mail);                    
                }
            }
        }
    }
}
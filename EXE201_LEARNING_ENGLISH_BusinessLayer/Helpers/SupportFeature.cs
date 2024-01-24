using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers
{
    public class SupportFeature
    {
        private static SupportFeature instance = null;
        public static SupportFeature Instance 
        {
            get { return instance?? (instance = new SupportFeature()); }
        }

        public void SendEmail(string receiver, string content, string title)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("tho.kieu@reso.vn", "1phanngocnga");

                using(MimeMessage message = new MimeMessage())
                {
                    message.From.Add(new MailboxAddress("DEVENG", "deveng.fpt@gmail.com"));
                    message.To.Add(new MailboxAddress("", receiver));

                    BodyBuilder builder = new BodyBuilder();
                    builder.TextBody = content;

                    message.Subject = title;
                    message.Body = builder.ToMessageBody();

                    client.Send(message);
                }
            }
        }
    }
}

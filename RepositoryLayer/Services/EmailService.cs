using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendMail(string email, string token)
        {
            using(SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("testingforapi586@gmail.com", "123456789q@");

                MailMessage messageObj = new MailMessage();
                messageObj.To.Add(email);
                messageObj.From = new MailAddress("testingforapi586@gmail.com");
                messageObj.Subject = "Password Reset Link";
                messageObj.Body = $"www.fundooNotes.com/rest-password/{token}";
                client.Send(messageObj);
            }
        }
    }
}

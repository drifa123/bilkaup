using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Bilkaup.Models.AccountViewModels;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sends an email from bilkaup@gmail.com
        /// </summary>
        /// <param name=”email”>
        /// Composed email that should be sent
        /// </param>
        public void SendEmail(EmailDTO email)
        {
            // TODO!! move network credential to secure location

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email.receiverEmail);
            mail.From = new MailAddress("bilkaup@gmail.com", email.head, System.Text.Encoding.UTF8);
            mail.Subject = email.subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = email.body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("bilkaup@gmail.com", "bilkaup123");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
        }

        /// <summary>
        /// Composes the email that is sent to admin when a carsale has applied for an account
        /// </summary>
        /// <param name=”carSale”>
        /// CarSaleViewModel with the information about the carSale
        /// </param>
        /// <returns>
        /// EmailDTO with the composed email
        /// </returns>
        public EmailDTO CreateAdminEmail(CarSaleViewModel carSale)
        {
            EmailDTO email = new EmailDTO
            {
                head = "Bílkaup",
                subject = "Nýr umsækjandi að kerfinu",
                body = "Ný umsókn hefur borist á Bílkaup.is. Þú getur séð umsóknina á þínum síðum. <br /> <br />" + 
                    "<strong>Bílasala:</strong> " + carSale.Name + "<br />" +
                    "<strong>Kennitala:</strong> " + carSale.SSN + "<br />" +
                    "<strong>E-mail:</strong> " + carSale.Email + "<br />"+
                    "<strong>Símanúmer:</strong> " + carSale.PhoneNum,
                receiverEmail = "bilkaup@gmail.com"
            };

            return email;
        }
        
        /// <summary>
        /// Composes the email that is sent to a carsale when it has been accepted and gets an email with username and password
        /// </summary>
        /// <param name=”carSale”>
        /// RegisterViewModel with the information about the carSale
        /// </param>
        /// <returns>
        /// EmailDTO with the composed email
        /// </returns>
        public EmailDTO CreateCarSaleEmail(RegisterViewModel carSale)
        {
            EmailDTO email = new EmailDTO
            {
                head = "Bílkaup",
                subject = "Aðgangur að Bilkaup.is",
                body = "Þú hefur fengið aðgang að Bilkaup.is og getur núna skráð þig inn á þínar síður í kerfinu. <br /> <br />" + 
                    "<strong>Notendanafn:</strong> " + carSale.Email + "<br />" +
                    "<strong>Lykilorð:</strong> " + carSale.Password + "<br />",
                receiverEmail = carSale.Email
            };

            return email;
        }
    }
}

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
    public class MockEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        
        public void SendEmail(EmailDTO email)
        {
           
        }

        public EmailDTO CreateAdminEmail(CarSaleViewModel carSale)
        {
            return new EmailDTO();
        }
        

        public EmailDTO CreateCarSaleEmail(RegisterViewModel carSale)
        {
            return new EmailDTO();
        }
    }
}

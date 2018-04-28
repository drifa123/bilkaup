using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bilkaup.Models.AccountViewModels;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// Sends an email from bilkaup@gmail.com
        /// </summary>
        void SendEmail(EmailDTO email);

        /// <summary>
        /// Composes the email that is sent to admin when a carsale has applied for an account
        /// </summary>
        EmailDTO CreateAdminEmail(CarSaleViewModel carSale);

        /// <summary>
        /// Composes the email that is sent to a carsale when it has been accepted and gets an email with username and password
        /// </summary>
        EmailDTO CreateCarSaleEmail(RegisterViewModel carSale);
    }
    
}

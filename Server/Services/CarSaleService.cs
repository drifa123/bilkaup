using System;
using System.Collections.Generic;
using Bilkaup.Models.AccountViewModels;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;
using Bilkaup.Repositories;

namespace Bilkaup.Services
{
    public class CarSaleService : ICarSaleService
    {
        private readonly ICarSaleRepository _repo;
        private readonly IEmailSender _email;
        
        public CarSaleService(ICarSaleRepository repo, IEmailSender email)
        {
            _repo = repo;
            _email = email;
        }

        /// <summary>
        /// Gets new carsale from controller and transfers data to repo
        /// Takes in the date  of today to get the time of application
        /// </summary>
        /// <param name=”newCarSale”>
        /// Parameter newCarSale requires a CarSaleViewModel argument.
        /// </param>
        /// <returns>
        /// Boolean
        /// True if the car was added
        /// False if something went wrong
        /// </returns>
        public bool AddCarSale(CarSaleViewModel newCarSale)
        {
            Console.WriteLine("IN SERVICE");
            DateTime now = new DateTime(); // Date at time of application

            CarSale carSale = new CarSale
            {
                Name = newCarSale.Name,
                SSN = newCarSale.SSN,
                Email = newCarSale.Email,
                Address = newCarSale.Address,
                PhoneNum = newCarSale.PhoneNum,
                Webpage = newCarSale.Webpage,
                Accepted = false,
                Active = false,
                DateOfApplication = now
            };

            var res = _repo.AddCarSale(carSale);

            if (res == true)
            {
                // If the carsale was added successfully, we want to send email to the admin to notify
                var email = _email.CreateAdminEmail(newCarSale);
                _email.SendEmail(email);

                return res;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets carsale by email from repo and returns it to controller
        /// </summary>
        /// <param name=”email”>
        /// The email of a carsale
        /// </param>
        /// <returns>
        /// CarSaleDTO with the response from repo
        /// </returns>
        public CarSaleDTO GetCarSaleByEmail(string email)
        {
            return _repo.GetCarSaleByEmail(email);
        }

        /// <summary>
        /// Gets waiting carsales from repo
        /// </summary>
        /// <returns>
        /// IEnumerable of CarSaleDTO with all waiting carsales
        /// </returns>
        public IEnumerable<AdminCarSaleDTO> GetAdminCarSales()
        {
            return _repo.GetAdminCarSales();
        }

        /// <summary>
        /// Gets carsale by id from repo and returns it to controller
        /// </summary>
        /// <param name=”id”>
        /// The id of a carsale
        /// </param>
        /// <returns>
        /// CarSaleDTO with the response from repo
        /// </returns>
        public CarSaleDTO GetCarSaleByID(int id)
        {
            return _repo.GetCarSaleByID(id);
        }

        /// <summary>
        /// Sends and id of a carsale to repo to accept it
        /// </summary>
        /// <param name=”id”>
        /// The id of a carsale
        /// </param>
        /// <returns>
        /// boolean with the response from repo
        /// </returns>
        public bool AcceptCarSale(int id)
        {
            return _repo.AcceptCarSale(id);
        }

        /// <summary>
        /// Sends and id of a carsale to repo to reovoke it's account
        /// </summary>
        /// <param name=”id”>
        /// The id of a carsale
        /// </param>
        /// <returns>
        /// boolean with the response from repo
        /// </returns>
        public bool RevokeCarSale(int id)
        {
            return _repo.RevokeCarSale(id);
        }

        /// <summary>
        /// Denies a carsale access and hard deletes it from the database
        /// </summary>
        /// <param name=”id”>
        /// The id of a carsale
        /// </param>
        /// <returns>
        /// boolean with the response from repo
        /// </returns>
        public bool DenyCarSale(int id)
        {
            return _repo.DenyCarSale(id);
        }

        public CarSaleDetailDTO GetCarSaleDetail(int id)
        {
            return _repo.GetCarSaleDetail(id);
        }
    }
}

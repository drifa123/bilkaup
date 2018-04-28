using System;
using System.Collections.Generic;
using Bilkaup.Models.AccountViewModels;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Services
{
    public interface ICarSaleService
    {

        /// <summary>
        /// Gets new carsale from controller and transfers data to repo
        /// Takes in the date  of today to get the time of application
        /// </summary>
        bool AddCarSale(CarSaleViewModel newCarSale);

        /// <summary>
        /// Gets waiting carsales from repo
        /// </summary>
        IEnumerable<AdminCarSaleDTO> GetAdminCarSales();

        /// <summary>
        /// Gets carsale by id from repo and returns it to controller
        /// </summary>
        CarSaleDTO GetCarSaleByID(int id);

        /// <summary>
        /// Sends and id of a carsale to repo to accept it
        /// </summary>
        bool AcceptCarSale(int id);

        /// <summary>
        /// Denies a carsale access and hard deletes it from the database
        /// </summary>
        bool DenyCarSale(int id);

        /// <summary>
        /// Sends and id of a carsale to repo to reovoke it's account
        /// </summary>
        bool RevokeCarSale(int id);

        /// <summary>
        /// Gets carsale by email from repo and returns it to controller
        /// </summary>
        CarSaleDTO GetCarSaleByEmail(string email);

        CarSaleDetailDTO GetCarSaleDetail(int id);
    }
}
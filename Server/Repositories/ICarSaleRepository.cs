using System.Collections.Generic;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;

namespace Bilkaup.Repositories
{
    /// <summary>
    /// Connects everything that has to do with carsales to the database.
    /// </summary>
    public interface ICarSaleRepository
    {
        /// <summary>
        /// Gets all the carsales from the database that have applied but have not yet
        /// been accepted.
        /// </summary>
        IEnumerable<AdminCarSaleDTO> GetAdminCarSales();

        /// <summary>
        /// Adds a new carsale into the database.
        /// This carsale has not yet been accepted or given an active account.
        /// </summary>
        bool AddCarSale(CarSale newCarSale);

        /// <summary>
        /// Gets the information for a specific carsale by using it's ID as key
        /// </summary>
        CarSaleDTO GetCarSaleByID(int id);

        /// <summary>
        /// Accepts a specific carsale by changing it's Accepted and Active status to true.
        /// Admin should be the only one allowed to do this!
        /// </summary>
        bool AcceptCarSale(int id);

        /// <summary>
        /// Revokes a specific carsale account by changing it's Active status to false.
        /// Admin should be the only one allowed to do this!
        /// </summary>
        bool RevokeCarSale(int id);

        /// <summary>
        /// Accepts a specific carsale by changing it's Accepted status to false.
        /// Admin should be the only one allowed to do this!
        /// </summary>
        bool DenyCarSale(int deniedCarSale);

        /// <summary>
        /// Gets the information for a specific carsale by using it's email as key
        /// </summary>
        CarSaleDTO GetCarSaleByEmail(string email);

        CarSaleDetailDTO GetCarSaleDetail(int id);
    }
}
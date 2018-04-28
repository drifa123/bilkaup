using System;
using System.Linq;
using System.Collections.Generic;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;

namespace Bilkaup.Repositories
{
    /// <summary>
    /// Connects everything that has to do with carsales to the database.
    /// </summary>
    /// <remarks>
    /// Every single function that has to do with the carsales goes through this controller.
    /// </remarks>
    public class CarSaleRepository : ICarSaleRepository
    {
        private readonly ApplicationDbContext _db;

        public CarSaleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all the carsales from the database that have applied but have not yet
        /// been accepted.
        /// </summary>
        /// <returns>
        /// List of AdminCarSaleDTO.
        /// </returns>
        public IEnumerable<AdminCarSaleDTO> GetAdminCarSales()
        {
            var carSales = (from cs in _db.CarSales
                            orderby cs.DateOfApplication ascending
                            select new AdminCarSaleDTO
                            {
                                ID = cs.ID,
                                Name = cs.Name,
                                SSN = cs.SSN,
                                Email = cs.Email,
                                PhoneNum = cs.PhoneNum,
                                Address = cs.Address,
                                Accepted = cs.Accepted,
                                Active = cs.Active,
                                DateOfApplication = cs.DateOfApplication,
                                WebPage = cs.Webpage
                            }).ToList();

            return carSales;
        }

        /// <summary>
        /// Adds a new carsale into the database.
        /// This carsale has not yet been accepted or given an active account.
        /// </summary>
        /// <param name=”newCarSale”>
        /// Parameter newCarSale requires a CarSale entity argument.
        /// </param>
        /// <returns>
        /// Boolean.
        /// False - If the CarSale entity is emtpy.
        /// True - If all goes well.
        /// </returns>
        public bool AddCarSale(CarSale newCarSale)
        {
            if (newCarSale == null)
            {
                return false;
            }

            _db.CarSales.Add(newCarSale);
            _db.SaveChanges();

            return true;
        }

        /// <summary>
        /// Gets the information for a specific carsale by using it's ID as key
        /// </summary>
        /// <param name=”id”>
        /// Parameter id requires a int argument.
        /// </param>
        /// <returns>
        /// CarSaleDTO
        /// </returns>
        public CarSaleDTO GetCarSaleByID(int id)
        {
            var carSale = (from cs in _db.CarSales
                            where cs.ID == id
                            select new CarSaleDTO
                            {
                                ID = cs.ID,
                                Name = cs.Name,
                                SSN = cs.SSN,
                                Email = cs.Email,
                                PhoneNum = cs.PhoneNum,
                                WebPage = cs.Webpage
                            }).SingleOrDefault();

            return carSale;
        }

        /// <summary>
        /// Gets the information for a specific carsale by using it's email as key
        /// </summary>
        /// <param name=”email”>
        /// Parameter email requires a CarSaleViewModel argument.
        /// </param>
        /// <returns>
        /// CarSaleDTO
        /// </returns>
        public CarSaleDTO GetCarSaleByEmail(string email)
        {
            var carSale = (from cs in _db.CarSales
                            where cs.Email == email
                            select new CarSaleDTO
                            {
                                ID = cs.ID,
                                Name = cs.Name,
                                SSN = cs.SSN,
                                Email = cs.Email,
                                PhoneNum = cs.PhoneNum,
                                WebPage = cs.Webpage
                            }).SingleOrDefault();

            return carSale;
        }

        /// <summary>
        /// Accepts a specific carsale by changing it's Accepted and Active status to true.
        /// Admin should be the only one allowed to do this!
        /// </summary>
        /// <param name=”id”>
        /// Parameter id requires a int argument.
        /// </param>
        /// <returns>
        /// Boolean.
        /// False - If the carsale with that specific id does not exist.
        /// True - If the changes were saved in the database.
        /// </returns>
        public bool AcceptCarSale(int id)
        {
            var carSale = _db.CarSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                carSale.Accepted = true;
                carSale.Active = true;
                _db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Revokes a specific carsale account by changing it's Active status to false.
        /// Admin should be the only one allowed to do this!
        /// </summary>
        /// <param name=”id”>
        /// Parameter id requires a int argument.
        /// </param>
        /// <returns>
        /// Boolean.
        /// False - If the carsale with that specific id does not exist.
        /// True - If the changes were saved in the database.
        /// </returns>
        public bool RevokeCarSale(int id)
        {
            var carSale = _db.CarSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                carSale.Active = false;
                _db.SaveChanges();
                
                return true;
            }
        }

        /// Denying a carsale of an account.ven an active account.
        /// </summary>
        /// <param name=”deniedCarSale”>
        /// This carsale has not yet been accepted or gi
        /// Parameter denied requires a CarSale entity argument.
        /// </param>
        /// <returns>
        /// Boolean.
        /// False - If the CarSale entity is emtpy.
        /// True - If all goes well.
        /// </returns>
        public bool DenyCarSale(int id)
        {
            var carSale = _db.CarSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                _db.CarSales.Remove(carSale);
                _db.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Gets all the carsales from the database that have been accepted and are now active.
        /// </summary>
        /// <returns>
        /// List of AdminCarSaleDTO.
        /// </returns>
        public IEnumerable<AdminCarSaleDTO> GetActiveCarSales()
        {
            var carSales = (from cs in _db.CarSales
                            where cs.Accepted == true
                            && cs.Active == true
                            && cs.Name != "Bílkaup"
                            select new AdminCarSaleDTO
                            {
                                ID = cs.ID,
                                Name = cs.Name,
                                SSN = cs.SSN,
                                Email = cs.Email,
                                PhoneNum = cs.PhoneNum,
                                Address = cs.Address,
                                Accepted = cs.Accepted,
                                Active = cs.Active,
                                DateOfApplication = cs.DateOfApplication,
                                WebPage = cs.Webpage
                            }).ToList();

            return carSales;
        }

        public CarSaleDetailDTO GetCarSaleDetail(int id)
        {
            DateTime now = DateTime.Now;

            var carSale = (from cs in _db.CarSales
                            where cs.ID == id
                            select new CarSaleDetailDTO
                            {
                                ID = id,
                                name = cs.Name,
                                ssn = cs.SSN,
                                email = cs.Email,
                                address = cs.Address,
                                phoneNum = cs.PhoneNum,
                                webpage = cs.Webpage,
                                openingHours = (from oh in _db.CarSaleOpenings
                                                where oh.CarSaleID == cs.ID
                                                select new OpeningHoursDTO
                                                {
                                                    monday = oh.Monday,
                                                    tuesday = oh.Tuesday,
                                                    wednesday = oh.Wednesday,
                                                    thursday = oh.Thursday,
                                                    friday = oh.Friday,
                                                    saturday = oh.Saturday,
                                                    sunday = oh.Sunday,
                                                    other = oh.OtherInfo
                                                }).SingleOrDefault(),
                                cars = (from c in _db.SaleInfos
                                        where c.CarSaleID == cs.ID
                                        && c.DateOfSale.Year < 2017
                                        select new CarCardDTO
                                        {
                                            serialNum = c.SerialNum,
                                            manufacturer = (from car in _db.Cars
                                                            join ma in _db.Manufacturers
                                                            on car.ManufacturerID equals ma.ID
                                                            where c.CarID == car.ID
                                                            select ma.Name).SingleOrDefault(),
                                            model = (from car in _db.Cars
                                                        join mo in _db.Models
                                                        on car.ModelID equals mo.ID
                                                        where c.CarID == car.ID
                                                        select mo.Name).SingleOrDefault(),
                                            modelType = (from car in _db.Cars
                                                            join mt in _db.ModelTypes
                                                            on car.ModelTypeID equals mt.ID
                                                            where c.CarID == car.ID
                                                            select mt.Name).SingleOrDefault(),
                                            imgLink = (from img in _db.Pictures
                                                        where c.SerialNum == img.CarSerialNum
                                                        && img.Primary == true
                                                        select img.Link).SingleOrDefault(),
                                            price = c.Price,
                                            offerPrice = c.OfferPrice,
                                            milage = (from car in _db.Cars
                                                        where car.ID == c.CarID
                                                        select car.Milage).SingleOrDefault(),
                                            transmission = (from car in _db.Cars
                                                            join trans in _db.Transmissions
                                                            on car.TransmissionID equals trans.ID
                                                            where car.ID == c.CarID
                                                            select trans.Name).SingleOrDefault(),
                                            onSite = c.OnSite,
                                            year = (from car in _db.Cars
                                                    where c.CarID == car.ID
                                                    select car.Year).SingleOrDefault(),
                                            regNum = (from car in _db.Cars
                                                        where c.CarID == car.ID
                                                        select car.LicenceNumber).SingleOrDefault(),
                                            dateOfSale = c.DateOnSale
                                        }).ToArray()
                            }).SingleOrDefault();
            
            return carSale;
        }
    }
}

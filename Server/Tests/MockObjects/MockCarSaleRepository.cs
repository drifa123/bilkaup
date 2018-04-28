using System;
using System.Collections.Generic;
using System.Linq;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;
using Bilkaup.Repositories;
using static Bilkaup.Tests.CarSaleTests;

namespace Bilkaup.Tests.MockObjects
{
	public class MockCarSaleRepository : ICarSaleRepository //where T : class
	{
		private static ICollection<CarSale> _carSales;
        private static ICollection<Car> _cars;
        private static ICollection<Manufacturer> _manufacturers;
        private static ICollection<Model> _models;
        private static ICollection<SaleInfo> _saleInfos;
        private static ICollection<ModelType> _modelTypes;
        private static ICollection<Transmission> _transmissions;
        private static ICollection<Picture> _pictures;
        private static ICollection<CarSaleOpening> _carSaleOpenings;

		public MockCarSaleRepository()
		{
			MockData data = new MockData();
			_carSales = data.CarSale;
            _cars = data.Car;
            _manufacturers = data.Manufacturer;
            _saleInfos = data.SaleInfos;
		}

		public IEnumerable<AdminCarSaleDTO> GetWaitingCarSales()
        {
            var carSales = (from cs in _carSales
                            where cs.Accepted == false
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

        public IEnumerable<AdminCarSaleDTO> GetActiveCarSales()
        {
            var carSales = (from cs in _carSales
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

        public bool AddCarSale(CarSale newCarSale)
        {
            if (newCarSale == null)
            {
                return false;
            }

            //_db.CarSales.Add(newCarSale);
            //_db.SaveChanges();

            return true;
        }

        IEnumerable<AdminCarSaleDTO> ICarSaleRepository.GetAdminCarSales()
        {
            var carSales = (from cs in _carSales
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

        bool ICarSaleRepository.AddCarSale(CarSale newCarSale)
        {
             if (newCarSale == null)
            {
                return false;
            }

            _carSales.Add(newCarSale);
            //_db.CarSales.Add(newCarSale);
            //_db.SaveChanges();

            return true;
        }

        CarSaleDTO ICarSaleRepository.GetCarSaleByID(int id)
        {
            var carSale = (from cs in _carSales
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

        bool ICarSaleRepository.AcceptCarSale(int id)
        {
            var carSale = _carSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                /*carSale.Accepted = true;
                carSale.Active = true;
                //_db.SaveChanges();*/
                return true;
            }
        }

        CarSaleDTO ICarSaleRepository.GetCarSaleByEmail(string email)
        {
            var carSale = (from cs in _carSales
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
        
        public bool DenyCarSale(int id)
        {
            var carSale = _carSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RevokeCarSale(int id)
        {
            var carSale = _carSales.SingleOrDefault(cs => cs.ID == id);

            if (carSale == null)
            {
                return false;
            }
            else
            {
                carSale.Accepted = false;
                carSale.Active = false;
                //_db.SaveChanges();
                return true;
            }
        }

        public CarSaleDetailDTO GetCarSaleDetail(int id)
        {
                        DateTime now = DateTime.Now;

            var carSale = (from cs in _carSales
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
                                openingHours = (from oh in _carSaleOpenings
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
                                cars = (from c in _saleInfos
                                        where c.CarSaleID == cs.ID
                                        && c.DateOfSale.Year < 2017
                                        select new CarCardDTO
                                        {
                                            serialNum = c.SerialNum,
                                            manufacturer = (from car in _cars
                                                            join ma in _manufacturers
                                                            on car.ManufacturerID equals ma.ID
                                                            where c.CarID == car.ID
                                                            select ma.Name).SingleOrDefault(),
                                            model = (from car in _cars
                                                        join mo in _models
                                                        on car.ModelID equals mo.ID
                                                        where c.CarID == car.ID
                                                        select mo.Name).SingleOrDefault(),
                                            modelType = (from car in _cars
                                                            join mt in _modelTypes
                                                            on car.ModelTypeID equals mt.ID
                                                            where c.CarID == car.ID
                                                            select mt.Name).SingleOrDefault(),
                                            imgLink = (from img in _pictures
                                                        where c.SerialNum == img.CarSerialNum
                                                        && img.Primary == true
                                                        select img.Link).SingleOrDefault(),
                                            price = c.Price,
                                            offerPrice = c.OfferPrice,
                                            milage = (from car in _cars
                                                        where car.ID == c.CarID
                                                        select car.Milage).SingleOrDefault(),
                                            transmission = (from car in _cars
                                                            join trans in _transmissions
                                                            on car.TransmissionID equals trans.ID
                                                            where car.ID == c.CarID
                                                            select trans.Name).SingleOrDefault(),
                                            onSite = c.OnSite,
                                            year = (from car in _cars
                                                    where c.CarID == car.ID
                                                    select car.Year).SingleOrDefault(),
                                            regNum = (from car in _cars
                                                        where c.CarID == car.ID
                                                        select car.LicenceNumber).SingleOrDefault(),
                                            dateOfSale = c.DateOnSale
                                        }).ToArray()
                            }).SingleOrDefault();

            return carSale;
        }
    }
}
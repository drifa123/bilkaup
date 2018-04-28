using System;
using System.Collections.Generic;
using System.Linq;
using Bilkaup.Models.EntityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bilkaup.Repositories;
using Bilkaup.Services;
using Bilkaup.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Bilkaup.Models.DTOModels;
using Bilkaup.Tests.MockObjects;
using API.Controllers;

namespace Bilkaup.Tests.MockObjects
{
    	public class MockCarRepository : ICarRepository //where T : class
	{
		private static ICollection<Car> _cars;
        private static ICollection<Manufacturer> _manufacturer;
        private static ICollection<Model> _models;
        private static ICollection<ModelType> _modelTypes;
        private static ICollection<FuelType> _fuelTypes;
        private static ICollection<FuelTypeCar> _fuelTypeCars;
        private static ICollection<Transmission> _transmissions;
        private static ICollection<CarSale> _carSales;
        private static ICollection<Drive> _drives;
        private static ICollection<SaleInfo> _saleInfos;
        private static ICollection<WheelDTO> _wheels;


		public MockCarRepository()
		{
			MockData data = new MockData();
			_cars = data.Car;
            _manufacturer = data.Manufacturer;
            _models = data.Model;
            _modelTypes = data.ModelType;
            _fuelTypes = data.FuelType;
            _carSales = data.CarSale;
            _saleInfos = data.SaleInfos;
            
		}

		public CarDetailDTO AddCar(CarViewModel car)
        {
            Console.WriteLine("Adding car to database");

            Car c = new Car();

            c.LicenceNumber = car.regNum;
            c.Year = car.year;
            c.ManufacturerID = 1;
            c.ModelID = 2;

            //_db.Cars.Add(c);
            //_db.SaveChanges();
            
            CarDetailDTO result = new CarDetailDTO();
            result.ID = c.ID;
            result.manufacturer = car.manufacturer;
            result.model = car.model;

            return result;
        }

        public int AddCar(Car car)
        {
            if(car == null)
            {
                return 0;
            }
            
            Console.WriteLine("Adding car to mock database");
            _cars.Add(car);
           // _cars.SaveChanges();

            return car.ID;
        }

        public int AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return 0;
            }

            //_cars.SaveChanges();
            _manufacturer.Add(manufacturer);

            return manufacturer.ID;
        }

        public int AddModel(Model model)
        {
            if(model == null)
            {
                return 0;
            }

            _models.Add(model);

            return model.ID;
        }

        public bool AddSellerInfo(SaleInfo newInfo)
        {
            if (newInfo == null)
            {
                return false;
            }

            _saleInfos.Add(newInfo);
           // _db.SaveChanges();

            return true;
        }

        public CarDetailDTO GetCarDetail(int carID, int serialNum)
        {
            var car = (from c in _cars
                        join mf in _manufacturer
                        on c.ManufacturerID equals mf.ID
                        join mod in _models
                        on c.ModelID equals mod.ID
                        where c.ID == carID
                        select new CarDetailDTO
                        {
                            ID = c.ID,
                            /*manufacturer = mf.Name,
                            model = mod.Name,
                            modelType = (from modT in _modelTypes
                                        where modT.ModelID == c.ModelID
                                        && modT.ManufID == c.ManufacturerID
                                        select modT.Name).SingleOrDefault(),
                            co2 = c.CO2,
                            color = c.Color,
                            weight = c.Weight,
                            year = c.Year,
                            milage = c.Milage,
                            nextCheckup = c.NextCheckup,
                            fuelTypes = (from fu in _fuelTypeCars
                                        join fc in _fuelTypes
                                        on fu.FuelTypeID equals fc.ID
                                        where fu.CarID == carID
                                        select fc.Fuel).ToList(),
                            cylinders = c.Cylinders,
                            cc = c.CC,
                            injection = c.Injection,
                            horsepower = c.Horsepower,
                            transmission = (from trans in _transmissions
                                            where trans.ID == c.TransmissionID
                                            select trans.Name).SingleOrDefault(),
                            drive = (from drive in _drives
                                    where drive.ID == c.DriveID
                                    select drive.Name).SingleOrDefault(),
                            seating = c.Seating,
                            doors = c.Doors,
                            dateSale = (from si in _saleInfos
                                        where si.SerialNum == serialNum
                                        select si.DateOnSale).SingleOrDefault(),
                            dateUpdate = (from si in _saleInfos
                                        where si.SerialNum == serialNum
                                        select si.DateOfUpdate).SingleOrDefault(),
                            carsale = (from cs in _carSales
                                        join si in _saleInfos
                                        on cs.ID equals si.CarSaleID
                                        where si.CarID == c.ID
                                        select new CarSaleBasicDTO
                                        {
                                            ID = cs.ID,
                                            name = cs.Name,
                                            email = cs.Email,
                                            phoneNum = cs.PhoneNum,
                                            webpage = cs.Webpage,
                                            address = cs.Address
                                        }).SingleOrDefault()*/
                        }).SingleOrDefault();

            return car;
        }

        public int GetCarIDBySerial(int serialNum)
        {
            var id = (from si in _saleInfos
                        where si.SerialNum == serialNum
                        select si.CarID).SingleOrDefault();

            return id;
        }

        /// <summary>
        /// Gets all unsold cars from the database.
        /// </summary>
        /// <returns>
        /// List of CarDetailDTO.
        /// </returns>
        public IEnumerable<CarCardDTO> GetCars()
        {
            return new List<CarCardDTO>
            {
                new CarCardDTO
                {
                    serialNum= 1,
                    manufacturer = "Toyota"
                },
                new CarCardDTO
                {
                    serialNum = 2,
                    manufacturer = "Honda"
                }
            };
        }

        public FilterDTO GetFilters()
        {
            var filters = new FilterDTO();

            filters.manufacturers = (from m in _manufacturer
                            select new ManufacturerFilterDTO
                            {
                                name = m.Name,
                                selected = false,
                                models = (from mo in _models
                                            where mo.ManufID == m.ID
                                            select new ModelFilterDTO{
                                                name = mo.Name,
                                                selected = false
                                            }).ToList()
                            }).ToList();
            return filters;
        }

        public int GetManufacturerIdByName(string manufacturer)
        {
            var manufacturerId = (from mf in _manufacturer
                                    where mf.Name == manufacturer
                                    select mf.ID
                                ).SingleOrDefault();
            
            if (manufacturerId > 0)
            {
                return manufacturerId;
            }
            else
            {
                return 0;
            }
        }

        public int GetModelIdByName(int manufacturerId, string model)
        {
            var modelId = (from m in _models
                            where m.Name == model
                            && m.ManufID == manufacturerId
                            select m.ID
                        ).SingleOrDefault();
            
            if (modelId > 0)
            {
                return modelId;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<CarCardDTO> SellCar(SaleInfo sold)
        {
            var carInfo = _saleInfos.SingleOrDefault(si => si.SerialNum == sold.SerialNum);

            if (carInfo == null)
            {
                return null;
            }
            else
            {
                carInfo.DateOfSale = sold.DateOfSale;
                //_db.SaveChanges();

                var cars = (from c in _saleInfos
                            where c.CarSaleID == carInfo.CarSaleID
                            && c.DateOfSale.Year < 2017
                            select new CarCardDTO
                            {
                                serialNum = c.SerialNum,
                                manufacturer = (from car in _cars
                                                join ma in _manufacturer
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
                                /*imgLink = (from img in _pictures
                                            where c.SerialNum == img.CarSerialNum
                                            && img.Primary == true
                                            select img.Link).SingleOrDefault(),*/
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
                            }).ToArray();
                
                return cars;
            }
        }

        IEnumerable<CarCardDTO> ICarRepository.GetCars()
        {
            throw new NotImplementedException();
        }

        IEnumerable<WheelDTO> ICarRepository.GetWheels()
        {
          throw new NotImplementedException();
        }

        IEnumerable<FuelTypeDTO> ICarRepository.GetFuelTypes()
        {
            throw new NotImplementedException();
        }

        IEnumerable<DriveSteeringDTO> ICarRepository.GetDriveSteeringInfos()
        {
            throw new NotImplementedException();
        }

        int ICarRepository.AddModel(Model model)
        {
            throw new NotImplementedException();
        }

        bool ICarRepository.AddSellerInfo(SaleInfo newInfo)
        {
            throw new NotImplementedException();
        }

        bool ICarRepository.AddWheelCar(WheelCar wc)
        {
            throw new NotImplementedException();
        }

        CarDetailDTO ICarRepository.GetCarDetail(int carID, int serialNum)
        {
            throw new NotImplementedException();
        }

        int ICarRepository.GetCarIDBySerial(int serialNum)
        {
            throw new NotImplementedException();
        }

        FilterDTO ICarRepository.GetFilters()
        {
            throw new NotImplementedException();
        }

        int ICarRepository.GetManufacturerIdByName(string manufacturer)
        {
            throw new NotImplementedException();
        }

        int ICarRepository.GetModelIdByName(int manufacturerId, string model)
        {
            throw new NotImplementedException();
        }

        IEnumerable<CarCardDTO> ICarRepository.SellCar(SaleInfo sold)
        {
            throw new NotImplementedException();
        }

        int ICarRepository.AddCar(Car car)
        {
            throw new NotImplementedException();
        }

        int ICarRepository.AddManufacturer(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        bool ICarRepository.AddFuelTypeCar(FuelTypeCar fc)
        {
            throw new NotImplementedException();
        }

        bool ICarRepository.AddDriveSteeringInfoCar(DriveSteeringInfoCar dc)
        {
            throw new NotImplementedException();
        }
    }
}

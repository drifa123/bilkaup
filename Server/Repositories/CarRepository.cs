using System;
using System.Collections.Generic;
using System.Linq;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Repositories
{
    /// <summary>
    /// Connects everything that has to do with cars to the database.
    /// </summary>
    /// <remarks>
    /// Every single function that has to do with the cars goes through this controller.
    /// </remarks>
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public int AddCar(Car car)
        {
            if (car == null)
            {
                return 0;
            }
            
            Console.WriteLine("Adding car to database");

            _db.Cars.Add(car);
            _db.SaveChanges();

            return car.ID;
        }

        public bool AddWheelCar(WheelCar wc)
        {
            if (wc == null)
            {
                return false;
            }
            
            Console.WriteLine("Adding wheelcar to database");

            _db.WheelCars.Add(wc);
            _db.SaveChanges();

            return true;
        }

        public bool AddFuelTypeCar(FuelTypeCar fc)
        {
            if (fc == null)
            {
                return false;
            }
            
            Console.WriteLine("Adding fuelTypeCar to database");

            _db.FuelTypeCars.Add(fc);
            _db.SaveChanges();

            return true;
        }

        public bool AddDriveSteeringInfoCar(DriveSteeringInfoCar dc)
        {
            if (dc == null)
            {
                return false;
            }
            
            Console.WriteLine("Adding DriveSteeringInfoCar to database");

            _db.DriveSteeringInfoCars.Add(dc);
            _db.SaveChanges();

            return true;
        }

        /// <summary>
        /// Gets all unsold cars from the database.
        /// </summary>
        /// <returns>
        /// List of CarCardDTO.
        /// </returns>
        public IEnumerable<CarCardDTO> GetCars()
        {
           var cars = (from c in _db.Cars
                            join m in _db.Manufacturers
                            on c.ManufacturerID equals m.ID
                            join t in _db.Models
                            on c.ModelID equals t.ID
                            select new CarCardDTO
                            {
                                serialNum = c.ID,
                                model = t.Name,
                                manufacturer = m.Name
                            }).ToList();
            return cars;
        }

        public IEnumerable<WheelDTO> GetWheels()
        {
            var wheel = (from a in _db.Wheels 
                            select new WheelDTO
                            {
                                id = a.ID,
                                name = a.Name
                            }).ToList();
            return wheel;
        }

        public IEnumerable<FuelTypeDTO> GetFuelTypes()
        {
            var fuelType = (from a in _db.FuelTypes
                                select new FuelTypeDTO
                                {
                                    id = a.ID,
                                    fuel = a.Fuel
                                }).ToList();
            return fuelType;
        }

        public IEnumerable<DriveSteeringDTO> GetDriveSteeringInfos()
        {
            var driveSteering = (from d in _db.DriveSteeringInfos
                                select new DriveSteeringDTO
                                {
                                    id = d.ID,
                                    name = d.Name
                                }).ToList();
            return driveSteering;
        }
        
        public int GetManufacturerIdByName(string manufacturer)
        {
            var manufacturerId = (from mf in _db.Manufacturers
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
            var modelId = (from m in _db.Models
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

        public int AddModel(Model model)
        {
            if (model == null)
            {
                return 0;
            }

            _db.Models.Add(model);
            _db.SaveChanges();

            return model.ID;
        }

        public int AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return 0;
            }

            _db.Manufacturers.Add(manufacturer);
            _db.SaveChanges();

            return manufacturer.ID;
        }

        public CarDetailDTO GetCarDetail(int carID, int serialNum)
        {
            var car = (from c in _db.Cars
                        join mf in _db.Manufacturers
                        on c.ManufacturerID equals mf.ID
                        join mod in _db.Models
                        on c.ModelID equals mod.ID
                        where c.ID == carID
                        select new CarDetailDTO
                        {
                            ID = c.ID,
                            manufacturer = mf.Name,
                            model = mod.Name,
                            modelType = (from modT in _db.ModelTypes
                                        where modT.ModelID == c.ModelID
                                        && modT.ManufID == c.ManufacturerID
                                        select modT.Name).SingleOrDefault(),
                            co2 = c.CO2,
                            color = c.Color,
                            weight = c.Weight,
                            year = c.Year,
                            milage = c.Milage,
                            nextCheckup = c.NextCheckup,
                            fuelTypes = (from fu in _db.FuelTypeCars
                                        join fc in _db.FuelTypes
                                        on fu.FuelTypeID equals fc.ID
                                        where fu.CarID == carID
                                        select fc.Fuel).ToList(),
                            wheels = (from wh in _db.WheelCars
                                        join wc in _db.Wheels
                                        on wh.WheelID equals wc.ID
                                        where wh.CarID == carID
                                        select wc.Name).ToList(),
                            cylinders = c.Cylinders,
                            cc = c.CC,
                            injection = c.Injection,
                            horsepower = c.Horsepower,
                            transmission = (from trans in _db.Transmissions
                                            where trans.ID == c.TransmissionID
                                            select trans.Name).SingleOrDefault(),
                            drive = (from drive in _db.Drives
                                    where drive.ID == c.DriveID
                                    select drive.Name).SingleOrDefault(),
                            seating = c.Seating,
                            doors = c.Doors,
                            dateSale = (from si in _db.SaleInfos
                                        where si.SerialNum == serialNum
                                        select si.DateOnSale).SingleOrDefault(),
                            dateUpdate = (from si in _db.SaleInfos
                                        where si.SerialNum == serialNum
                                        select si.DateOfUpdate).SingleOrDefault(),
                            carsale = (from cs in _db.CarSales
                                        join si in _db.SaleInfos
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
                                        }).SingleOrDefault()
                        }).SingleOrDefault();

            return car;
        }

        public bool AddSellerInfo(SaleInfo newInfo)
        {
            if (newInfo == null)
            {
                return false;
            }

            _db.SaleInfos.Add(newInfo);
            _db.SaveChanges();

            return true;
        }

        public FilterDTO GetFilters()
        {
            var filters = new FilterDTO();

            filters.manufacturers = (from m in _db.Manufacturers
                            select new ManufacturerFilterDTO
                            {
                                name = m.Name,
                                selected = false,
                                models = (from mo in _db.Models
                                            where mo.ManufID == m.ID
                                            select new ModelFilterDTO{
                                                name = mo.Name,
                                                selected = false
                                            }).ToList()
                            }).ToList();
            return filters;
        }
        
        public int GetCarIDBySerial(int serialNum)
        {
            var id = (from si in _db.SaleInfos
                        where si.SerialNum == serialNum
                        select si.CarID).SingleOrDefault();

            return id;
        }

        /// <summary>
        /// Marks car as sold with date
        /// </summary>
        /// <param name=”sold”>
        /// Parameter sold requires a SaleInfo object argument.
        /// </param>
        /// <returns>
        /// NULL - If the car does not exist
        /// List of CarCardDTO - List of cars that are not sold
        /// </returns>
        public IEnumerable<CarCardDTO> SellCar(SaleInfo sold)
        {
            var carInfo = _db.SaleInfos.SingleOrDefault(si => si.SerialNum == sold.SerialNum);

            if (carInfo == null)
            {
                return null;
            }
            else
            {
                carInfo.DateOfSale = sold.DateOfSale;
                _db.SaveChanges();

                var cars = (from c in _db.SaleInfos
                            where c.CarSaleID == carInfo.CarSaleID
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
                            }).ToArray();
                
                return cars;
            }
        }
    }
}
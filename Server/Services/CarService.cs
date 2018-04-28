using System;
using System.Collections.Generic;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;
using Bilkaup.Repositories;

namespace Bilkaup.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;
        
        public CarService(ICarRepository repo)
        {
            _repo = repo;
        }

        private int Count(IEnumerable<int> array)
        {
            int count = 0;
            foreach (var item in array)
            {
                count++;
            }

            return count;
        }

        public int AddCar(CarViewModel car)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("AddCar: Adding car to repository... IN CAR SERVICE.CS");
            Console.WriteLine("===================================");

            var manufacturerId = CheckManufacturerByName(car.manufacturer);
            var modelId = CheckModelByName(manufacturerId, car.model);
            
            int fuelTypeCount = Count(car.fuelType);

            Car newCar = new Car()
            {
                LicenceNumber = car.regNum,
                ManufacturerID = manufacturerId,
                ModelID = modelId,
                Year = car.year,
                CO2 = car.co2,
                Color = car.color,
                Status = car.status,
                Doors = car.doors,
                Seating = car.seating,
                Milage = car.driven,
                Cylinders = car.cylinders,
                Horsepower = car.horsepower,
                Injection = car.injection,
                CC = car.cc,
                Weight = car.weight,
                Hybrid = fuelTypeCount > 1,
                DriveID = car.drive,
                TransmissionID = car.transmission
            };


            var carID = _repo.AddCar(newCar);
            DateTime localDate = DateTime.Now;

            Console.WriteLine("=====================");
            Console.WriteLine("Inni í AddCar service.cs");
            Console.WriteLine("=====================");

            SaleInfo newInfo = new SaleInfo()
            {
                CarSaleID = car.carSaleId,
                CarID = carID,
                DateOnSale = localDate,
                DateOfUpdate = localDate,
                SellerID = 0,
                Price = car.price,
                OnSite = car.onSite
            };

            var check = _repo.AddSellerInfo(newInfo);

            Console.WriteLine("=====================");
            Console.WriteLine("Adding Wheel types to Car - in CarService.cs");
            Console.WriteLine("=====================");

            foreach (var wheelId in car.wheel)
            {
                WheelCar wc = new WheelCar()
                {
                    CarID = carID,
                    WheelID = wheelId,
                    Quantity = 4
                };

                _repo.AddWheelCar(wc);
                Console.WriteLine("  Adding " + wc.ToString());
            }

            foreach (var fuelId in car.fuelType)
            {
                FuelTypeCar fc = new FuelTypeCar()
                {
                    CarID = carID,
                    FuelTypeID = fuelId,
                };

                _repo.AddFuelTypeCar(fc);
                Console.WriteLine("  Adding " + fc.ToString());
            }

            foreach (var driveSteeringId in car.driveSteering)
            {
                DriveSteeringInfoCar dc = new DriveSteeringInfoCar()
                {
                    CarID = carID,
                    DriveSteeringID = driveSteeringId,
                };

                _repo.AddDriveSteeringInfoCar(dc);
                Console.WriteLine("Adding DRIVESTEERING " + dc.ToString());
            }

            return carID;
        }

        public int CheckManufacturerByName(string manufacturer)
        {
            var manufacturerID = _repo.GetManufacturerIdByName(manufacturer);

            // If the manufacturer doesn't exist we add him to the DB
            if (manufacturerID == 0)
            {
                Manufacturer manu = new Manufacturer()
                {
                    Name = manufacturer
                };

                manufacturerID = _repo.AddManufacturer(manu);
            }

            return manufacturerID;
        }

        public int CheckModelByName(int manufacturerId, string model)
        {
            var modelID = _repo.GetModelIdByName(manufacturerId, model);

            // If the manufacturer doesn't exist we add him to the DB
            if (modelID == 0)
            {
                Model mod = new Model()
                {
                    Name = model,
                    ManufID = manufacturerId
                };

                modelID = _repo.AddModel(mod);
            }

            return modelID;
        }

        public CarDetailDTO GetCarBySerialNum(int serialNum)
        {
            var carId = _repo.GetCarIDBySerial(serialNum);

            
            return _repo.GetCarDetail(carId, serialNum);
        }

        public IEnumerable<CarCardDTO> GetCars()
        {
            var cars = _repo.GetCars();

            return cars;
        }
        
        public IEnumerable<WheelDTO> GetWheels()
        {
            var wheels = _repo.GetWheels();

            return wheels;
        }

        public IEnumerable<FuelTypeDTO> GetFuelTypes()
        {
            var fuelTypes = _repo.GetFuelTypes();

            return fuelTypes;
        }

        public IEnumerable<DriveSteeringDTO> GetDriveSteeringInfos()
        {
            var driveSteeringInfo = _repo.GetDriveSteeringInfos();

            return driveSteeringInfo;
        }

        public FilterDTO GetFilters()
        {
            return _repo.GetFilters();
        }

        public IEnumerable<CarCardDTO> SellCar(int serialNum)
        {
            DateTime localDate = DateTime.Now;

            SaleInfo soldCar = new SaleInfo()
            {
                DateOfSale = localDate,
                SerialNum = serialNum
            };

            return _repo.SellCar(soldCar);
        }
    }
}
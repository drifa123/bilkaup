using System;
using System.Collections.Generic;
using System.Linq;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;
using Bilkaup.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bilkaup.Tests.MockObjects
{
	public class MockData
	{
		public List<CarSale> CarSale = new List<CarSale>
		{
			new CarSale {ID = 1, Name = "Bilasala1", SSN = "123456789", Email = "carsale1@mail.com", PhoneNum = "1234567", Address = "Address1", Accepted = true, Active = true},
			new CarSale {ID = 2, Name = "Bilasala2", SSN = "987654321", Email = "carsale2@mail.com", PhoneNum = "7654321", Address = "Address2", Accepted = true, Active = true},
			new CarSale {ID = 1, Name = "Bilasala3", SSN = "234567890", Email = "carsale3@mail.com", PhoneNum = "2345678", Address = "Address3", Accepted = false, Active = false},
		};

		public List<Car> Car = new List<Car>
		{
			new Car {ID = 1, LicenceNumber = "PEY45", ManufacturerID = 1, ModelID = 2, Year = "10.05.2010"},
			new Car {ID = 3, LicenceNumber = "PP676", ManufacturerID = 2, ModelID = 3, Year = "18.02.2000"}
		};
		
		// Using in AddCar Test
		public CarViewModel okCar = new CarViewModel
		{
			manufacturer = "MAZDA", year = "30.06.2008", model= "6", regNum = "RZZ23"
		};

		// Using in AddCar Test
		public CarViewModel invalidAddCar = new CarViewModel
		{
			manufacturer = "", model = ""
		};


		// Using in AddCarSale Test
		public CarSaleViewModel okAddCarSale = new CarSaleViewModel
		{
			Name = "Bilasala4", SSN = "1111111111", Email = "carsale4@mail.com", PhoneNum = "1234567", Address = "Address4"
		};
		
		// Using in AddCarSale Test
		public CarSaleViewModel invalidAddCarSale = new CarSaleViewModel
		{
			Name = "Bilasala1", SSN = "1111111111", Email = "carsale1@mail.com", PhoneNum = "1234567", Address = "Address1"
		};

		// Using in Register Test
		public CarSaleDTO registerCarSale = new CarSaleDTO
		{
			Name = "Bilasala3", SSN = "1111111111", Email = "carsale3@mail.com", PhoneNum = "1234567", Address = "Address3"
		};

		public List<SaleInfo> SaleInfos = new List<SaleInfo>
		{
			new SaleInfo {CarID = 1, SerialNum = 1, CarSaleID = 1, SellerID = 1, Price = 2400000, DateOnSale = System.DateTime.Today, DateOfSale = System.DateTime.Now, DateOfUpdate = System.DateTime.Today, OnSite = true},
			new SaleInfo {CarID = 1, SerialNum = 2, CarSaleID = 2, SellerID = 3, Price = 5000000, DateOnSale = System.DateTime.Today, DateOfSale = System.DateTime.Now, DateOfUpdate = System.DateTime.Today, OnSite = false},
			new SaleInfo {CarID = 2, SerialNum = 3, CarSaleID = 1, SellerID = 6, Price = 230000, DateOnSale = System.DateTime.Today, DateOfSale = System.DateTime.Now, DateOfUpdate = System.DateTime.Today, OnSite = true},
		};

		public List<Manufacturer> Manufacturer = new List<Manufacturer>
		{
			new Manufacturer {ID = 1, Name = "TOYOTA"},
			new Manufacturer {ID = 2, Name = "NISSAN"},
			new Manufacturer {ID = 3, Name = "FORD"},
			new Manufacturer {ID = 4, Name = "KIA"}
		};

		public List<Model> Model = new List<Model>
		{
			new Model {ID = 1, ManufID = 1, Name = "YARIS"},
			new Model {ID = 2, ManufID = 1, Name = "RAV4"},
			new Model {ID = 3, ManufID = 2, Name = "MICRA"},
			new Model {ID = 4, ManufID = 3, Name = "EXPLORER"}
		};

		public List<ModelType> ModelType = new List<ModelType>
		{
			new ModelType {ID = 1, ManufID = 1, ModelID = 1, Name = "SOL"}
		};

		public List<FuelType> FuelType = new List<FuelType>
		{
			new FuelType {ID = 1, Fuel = "Bensín"},
			new FuelType {ID = 1, Fuel = "Dísel"},
			new FuelType {ID = 1, Fuel = "Rafmagn"},
			new FuelType {ID = 1, Fuel = "Metan"},
			new FuelType {ID = 1, Fuel = "Vetni"}
		};

		public WheelDTO wheel = new WheelDTO
		{
			id= 1

		};

	}
}

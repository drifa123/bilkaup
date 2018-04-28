using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using API.Controllers;
using Bilkaup.Models.DTOModels;
using Bilkaup.Repositories;
using Bilkaup.Services;
using Bilkaup.ElasticSearch;
using Bilkaup.Models.ViewModels;
using Bilkaup.Tests.MockObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;

namespace Bilkaup.Tests
{
    [TestClass]
    public class CarTests
	{
	    private ICarRepository _repo;
	    private ICarService _carService;
	    private CarController _carController;
	    private MockData _data;
		private ESClientProvider _esClientProvider;
		private IHttpContextAccessor _httpContextAccessor;

		[TestInitialize]    
		public void Initialize()
		{
			_repo = new MockCarRepository();
			_carService = new CarService(_repo);
			_carController = new CarController(_carService, _esClientProvider);
			_carController.ControllerContext = new ControllerContext();
			_carController.ControllerContext.HttpContext = new DefaultHttpContext();
			_data = new MockData();
		}

		// Car test
		// TODO Finish when all car info has been inserted in DB.
		// Testing GET "/api/car"
		[TestMethod]
		public void GetCars()
		{
			// Arrange:

			// Act:
			var response = _carController.GetCars();
			OkObjectResult result = response as OkObjectResult;
			List<CarCardDTO> car = result.Value as List<CarCardDTO>;

			// Assert
			Assert.IsInstanceOfType(response, typeof(OkObjectResult));
			Assert.AreEqual(car.Count(), 2);
		}

		// Testing DELETE api/car/{serialNum}
		[TestMethod]
		public void SellCar()
		{
			// Arrange			
			var correctSerialNum = 1;
			var notFoundtSerialNum = 100;
			var preFailSerialNum = 0;

			// Act
			var notFoundResponse = _carController.SellCarFunction(notFoundtSerialNum);
			NotFoundResult notFoundResult = notFoundResponse as NotFoundResult;

			var okResponse = _carController.SellCarFunction(correctSerialNum);
			OkObjectResult okResult = okResponse as OkObjectResult;

			var preConFailedResponse = _carController.SellCarFunction(preFailSerialNum);
			StatusCodeResult preConFailedResult = preConFailedResponse as StatusCodeResult;

			// Assert
			Assert.IsInstanceOfType(notFoundResponse, typeof(NotFoundResult));
			Assert.IsInstanceOfType(okResponse, typeof(OkObjectResult));
			Assert.IsInstanceOfType(preConFailedResponse, typeof(StatusCodeResult));
		}

		[TestMethod]
		public void GetCarBySerialNum()
		{
			// Arrange
			var illegalSeriaNum = 0;
			var correctSerialNum = 1;
			var notFoundSerialNum = 100;

			// Act
			var illegalRequestResult = _carController.GetCarBySerialNum(illegalSeriaNum);
			BadRequestResult illegalResult = illegalRequestResult as BadRequestResult;

			var badRequestResult = _carController.GetCarBySerialNum(notFoundSerialNum);
			BadRequestResult badResult = badRequestResult as BadRequestResult;

			var okResponse = _carController.GetCarBySerialNum(correctSerialNum);
			OkObjectResult okResult = okResponse as OkObjectResult;

			// Assert
			Assert.IsInstanceOfType(badRequestResult, typeof(BadRequestResult));
			Assert.IsInstanceOfType(okResponse, typeof(OkObjectResult));
		}

		 [TestMethod]
		public void AddCarFunction()
		{
			 
			// Arrange:
			var okCar = _data.okCar;
			CarViewModel invalidCar = null;

			// Act:
			var okResponse = _carController.AddCarFunction(okCar);
			CreatedAtActionResult okResult = okResponse as CreatedAtActionResult;

			var badResponse = _carController.AddCarFunction(invalidCar);
			BadRequestResult badResult = badResponse as BadRequestResult;

			var invalidModelStateResponse = _carController.AddCarFunction(invalidCar);
			StatusCodeResult badStatus = invalidModelStateResponse as StatusCodeResult;
			
			// Assert:
		
			Assert.IsInstanceOfType(okResponse, typeof(CreatedAtActionResult));
			Assert.IsInstanceOfType(badResponse, typeof(BadRequestResult));
			Assert.IsInstanceOfType(badResponse, typeof(StatusCodeResult));
		}
	}
}






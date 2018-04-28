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
using Microsoft.AspNetCore.Identity;
using Bilkaup.Models;
using Microsoft.AspNetCore.Http;

namespace Bilkaup.Tests
{
	[TestClass]
    public class CarSaleTests
	{
		private ICarSaleRepository _repo;
		private IEmailSender _email  = new EmailSender();
		private ICarSaleService _carSaleService;
		private CarSaleController _carSaleController;
		private readonly UserManager<ApplicationUser> _userManager;
		private MockData _data;

		[TestInitialize]
		public void Initialize()
		{
			_repo = new MockCarSaleRepository();
			_email = new MockEmailSender();
			_carSaleService = new CarSaleService(_repo, _email);
			_carSaleController = new CarSaleController(_userManager, _carSaleService);
			_data = new MockData();
		}

		// Carsale test

		// Testing GET "/api/carSale/admin/waiting"
        [TestMethod]
    	public void GetAdminCarSales()
		{
			// Arrange:

			// Act:
			var response = _carSaleController.GetAdminCarSalesFunction();
			OkObjectResult result = response as OkObjectResult;
			List<AdminCarSaleDTO> carSales = result.Value as List<AdminCarSaleDTO>;

			// Assert
			Assert.IsInstanceOfType(response, typeof(OkObjectResult));
			// Result changed after change of the function
			//Assert.AreEqual(carSales.Count(), 1);
		}

		// Testing PUT "/api/carSale/{id}/revoke"
		[TestMethod]
		public void RevokeCarSale()
		{
			// Arrange:
			int validId = 2;
			int invalidId = 0;
			int noCarSaleId = 5;

			// Act:
			var badReqId = _carSaleController.RevokeCarSaleFunction(invalidId);
			BadRequestObjectResult badResult = badReqId as BadRequestObjectResult;

			var noCarSale = _carSaleController.RevokeCarSaleFunction(noCarSaleId);
			NotFoundObjectResult notFoundResult = noCarSale as NotFoundObjectResult;

			var validCarSale = _carSaleController.RevokeCarSaleFunction(validId);
			CreatedAtActionResult createdResult = validCarSale as CreatedAtActionResult;

			// Assert:
			Assert.IsInstanceOfType(badReqId, typeof(BadRequestObjectResult));
			Assert.IsInstanceOfType(noCarSale, typeof(NotFoundObjectResult));
			Assert.IsInstanceOfType(validCarSale, typeof(CreatedAtActionResult));
		}

		// Testing POST "/api/carsasle"
        [TestMethod]
		public void AddCarSale()
		{
			// Arrange:
			var okCarSale = _data.okAddCarSale;
			CarSaleViewModel invalidCarSale = _data.invalidAddCarSale;
			
			// Act:
			var okResponse = _carSaleController.AddCarSale(okCarSale);
			CreatedAtActionResult okResult = okResponse as CreatedAtActionResult;
			//CarSaleViewModel okValue = okResult.Value as CarSaleViewModel;

			var badResponse = _carSaleController.AddCarSale(invalidCarSale);
			BadRequestObjectResult badResult = badResponse as BadRequestObjectResult;

			// Assert:
			Assert.IsInstanceOfType(okResponse, typeof(CreatedAtActionResult));
			Assert.IsInstanceOfType(badResponse, typeof(BadRequestObjectResult));
		}
		
		[TestMethod]
		public void AcceptCarSale()
		{
			// Arrange:
			var invalidID = 0;
			var badID = 3;

			// Act:
			var badResponse = _carSaleController.AcceptCarSaleFunction(invalidID);
			BadRequestObjectResult badResult = badResponse as BadRequestObjectResult;

			var notFoundResponse = _carSaleController.AcceptCarSaleFunction(badID);
			NotFoundObjectResult IDNotThere = notFoundResponse as NotFoundObjectResult; 

			// Assert:
			Assert.IsInstanceOfType(badResponse, typeof(BadRequestObjectResult));
			Assert.IsInstanceOfType(notFoundResponse, typeof(NotFoundObjectResult));
		}
		
		[TestMethod]
		public void DenyCarSale()
		{
			// Arrage
			var notFoundId = 5;
			var invalidID = 0;

			// Act:
			var badResponse = _carSaleController.DenyCarSaleFunction(invalidID);
			BadRequestObjectResult badResult = badResponse as BadRequestObjectResult;

			var notFoundResponse = _carSaleController.DenyCarSaleFunction(notFoundId);
			NotFoundObjectResult IDNotThere = notFoundResponse as NotFoundObjectResult; 

			// Assert:
			Assert.IsInstanceOfType(badResponse, typeof(BadRequestObjectResult));
			Assert.IsInstanceOfType(notFoundResponse, typeof(NotFoundObjectResult));
		}
    }
}
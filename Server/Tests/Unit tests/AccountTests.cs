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
using Microsoft.Extensions.Logging;
using Bilkaup.Controllers;
using Bilkaup.Models;


// Scraping this for the time being!


namespace Bilkaup.Tests
{
	/*
	[TestClass]
    public class AccountTests
	{
		private IEmailSender _email;
		private MockData _data;
        private AccountController _accountController;
        private UserManager<ApplicationUser> _userManager;
        private  SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private  IIdentityService _identityService;


		[TestInitialize]
		public void Initialize()
		{
            
			_email = new MockEmailSender();
			_data = new MockData();
            _identityService = new IdentityService();
            //_accountController = new AccountController(_userManager, _signInManager,  _email, _logger,  _identityService);

        }


		// Carsale test

		// Testing GET "/api/carSale"
        [TestMethod]
    	public void Register()
		{
			// Arrange:
            var carSale = _data.registerCarSale;

			// Act:
            var response = _accountController.Register(carSale);
			IActionResult result = response as IActionResult;

			// Assert
            Assert.IsInstanceOfType(result, typeof(IActionResult));
		}
    }*/
}

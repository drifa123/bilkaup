using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Bilkaup.Controllers;
using Bilkaup.Extension;
using Bilkaup.Models;
using Bilkaup.Models.ViewModels;
using Bilkaup.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    /// <summary>
    /// The controller that receives all the Http requests for the CarSale
    /// </summary>
    /// <remarks>
    /// Contains the GET, POST, PUT, DELETE requests for the CarSales.
    /// Each type of request is under it's own category.
    /// </remarks>
    [Route("api/[controller]")]
    public class CarSaleController : Controller
    {
        private readonly ICarSaleService _carSaleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string ADMIN = "Admin";
        private readonly string CARSALE = "Carsale";
        
        public CarSaleController(UserManager<ApplicationUser> userManager, ICarSaleService carSaleService)
        {
            _carSaleService = carSaleService;
            _userManager = userManager;
        }

        /*
        * --------------------------------- GET REQUESTS ---------------------------------
        */

        [HttpGet("{id:int}")]
        public IActionResult GetCarSaleDetail(int id)
        {
            // Check if logged in user is authenticaded as Carsale
            if (HttpContext.Request.Cookies["User"] == CARSALE)
            {
                return GetCarSaleDetailFunction(id);
            }

            return Unauthorized();
        }

        public IActionResult GetCarSaleDetailFunction(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var exists = _carSaleService.GetCarSaleByID(id);

            if (exists == null)
            {
                return BadRequest();
            }

            var carSale = _carSaleService.GetCarSaleDetail(id);
            
            return Ok(carSale);
        }

        /// <summary>
        /// Get api/admin/carSale
        /// Gets a list of all car sales that are waiting to be accepted
        /// </summary>
        /// <returns>
        /// An IActionResult.
        /// 200 - OK with list of the car sales.
        /// </returns>
        [HttpGet("admin/carSales")]
        public IActionResult GetAdminCarSales()
        {
            // Check if logged in user is authenticaded as Admin
            if (HttpContext.Request.Cookies["User"] == ADMIN)
            {
                return GetAdminCarSalesFunction();
            }
            
            return Unauthorized();
        }
        public IActionResult GetAdminCarSalesFunction()
        {
            var carSales = _carSaleService.GetAdminCarSales();
            
            return Ok(carSales);
        }

        /*
        * --------------------------------- POST REQUESTS ---------------------------------
        */
        
        /// <summary>
        /// POST api/carsale
        /// Posts a new carsale that has applied for a membership of Bilkaup
        /// </summary>
        /// <param name=”newCarSale”>
        /// Parameter newCarSale requires a CarSaleViewModel argument.
        /// </param>
        /// <returns>
        /// An IActionResult.
        /// 400 - Bad Request if the ViewModel is empty.
        /// 412 - Precondition Failed if the ViewModel is not valid.
        /// 200 - OK if all goes well.
        /// </returns>
        [HttpPost("")]
        public IActionResult AddCarSale([FromBody] CarSaleViewModel newCarSale)
        {
            Console.WriteLine("CARSALE: " + newCarSale.Name);
            
            if (newCarSale == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }

            var checkIfExists = _carSaleService.GetCarSaleByEmail(newCarSale.Email);
            if (checkIfExists != null)
            {
                return BadRequest("This carsale is already in the database");
            }

            var res = _carSaleService.AddCarSale(newCarSale);

            if (res == false)
            {
                return BadRequest("Unable to post"); // Add exception here!
            }

            return CreatedAtAction("Registered", newCarSale); // TODO: Better way to do this? Return something else?
        }

        /*
        * --------------------------------- PUT REQUESTS ---------------------------------
        */
        
        /// <summary>
        /// PUT /api/carsale/{id}/accept
        /// Changes the status of a carsale to accepted
        /// </summary>
        /// <param name=”id”>
        /// Parameter id to identify carsale in database
        /// </param>
        /// <returns>
        /// An IActionResult.
        /// 400 - Bad Request if the id is not valid or if something went wrong.
        /// 404 - Not Found if the car sale is not in the database
        /// 201 - Created if all goes well.
        /// </returns>
        [HttpPut("{id:int}/accept")]
        public IActionResult AcceptCarSale(int id)
        {
            // Check if logged in user is authenticaded as Admin
            if(HttpContext.Request.Cookies["User"] == ADMIN)
            {
                return AcceptCarSaleFunction(id);
            }
            return Unauthorized();
        }
        public IActionResult AcceptCarSaleFunction(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID is not valid");
            }

            var carSale = _carSaleService.GetCarSaleByID(id);

            if (carSale == null)
            {
                return NotFound("This carsale is not in database");
            }

            var res = _carSaleService.AcceptCarSale(id);
            
            if (res == true)
            {
                var carSales = _carSaleService.GetAdminCarSales();
                return CreatedAtAction("Accepted!", carSales);
            }
            else
            {
                return BadRequest("Could not accept the carsale");
            }
        }

        /// <summary>
        /// PUT /api/carsale/id
        /// Changes the status of a carsale to denied
        /// </summary>
        /// <param name=”id”>
        /// Parameter id to identify carsale in database
        /// </param>
        /// <returns>
        /// An IActionResult.
        /// 400 - Bad Request if the id is not valid or if something went wrong.
        /// 404 - Not Found if the car sale is not in the database
        /// 201 - Removed if all goes well.
        /// </returns>
        [HttpDelete("{id:int}")]
        public IActionResult DenyCarSale(int id)
        {
            // Check if logged in user is authenticaded as Admin
            if (HttpContext.Request.Cookies["User"] == ADMIN)
            {
                return DenyCarSaleFunction(id);
            }

            return Unauthorized();
        }
        public IActionResult DenyCarSaleFunction(int id)
        {
            if (id < 1)
                {
                    return BadRequest("ID is not valid");
                }

                var carSale = _carSaleService.GetCarSaleByID(id);

                if (carSale == null)
                {
                    return NotFound("This carsale is not in database");
                }

                var res = _carSaleService.DenyCarSale(id);
                
                if (res == true)
                {
                    var carSales = _carSaleService.GetAdminCarSales();
                    return CreatedAtAction("Denied!", carSales);
                }
                else
                {
                    return BadRequest("Could not deny the carsale");
                }
        }

        /// <summary>
        /// PUT /api/carsale/{id}/revoke
        /// Changes the activation of a carsale to inactive
        /// </summary>
        /// <param name=”id”>
        /// Parameter id to identify carsale in database
        /// </param>
        /// <returns>
        /// An IActionResult.
        /// 400 - Bad Request if the id is not valid or if something went wrong.
        /// 404 - Not Found if the car sale is not in the database
        /// 201 - Created if all goes well.
        /// </returns>
        [HttpPut("{id:int}/revoke")]
        public IActionResult RevokeCarSale(int id)
        {
            // Check if logged in user is authenticaded as Admin
            if (HttpContext.Request.Cookies["User"] == ADMIN)
            {
                return RevokeCarSaleFunction(id);
            }

            return Unauthorized();
        }

        public IActionResult RevokeCarSaleFunction(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID is not valid");
            }

            var carSale = _carSaleService.GetCarSaleByID(id);

            if (carSale == null)
            {
                return NotFound("This carsale is not in database");
            }

            var res = _carSaleService.RevokeCarSale(id);
            
            if (res == true)
            {
                var carSales = _carSaleService.GetAdminCarSales();
                return CreatedAtAction("Revoked!", carSales);
            }
            else
            {
                return BadRequest("Could not accept the carsale");
            }
        }

        /*
        * --------------------------------- DELETE REQUESTS ---------------------------------
        */
    }
}

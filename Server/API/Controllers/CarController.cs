using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Bilkaup.Controllers;
using Bilkaup.ElasticSearch;
using Bilkaup.Models;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.ViewModels;
using Bilkaup.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Nest;

namespace API.Controllers
{
    /// <summary>
    /// The controller that receives all the Http requests for the Cars
    /// </summary>
    /// <remarks>
    /// Contains the GET, POST, PUT, DELETE requests for the Cars.
    /// Each type of request is under it's own category.
    /// </remarks>
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private ESClientProvider _esClientProvider;
        private readonly string CARSALE = "Carsale";
        
        public CarController(ICarService carService, ESClientProvider esClientProvider)
        {
            _carService = carService;
            _esClientProvider = esClientProvider;
        }

        /*
        * --------------------------------- GET REQUESTS ---------------------------------
        */

        /// <summary>
        /// GET api/car
        /// Gets all the unsold cars in the database.
        /// </summary>
        /// <returns>
        /// An IActionResult.
        /// 200 - OK if all goes well.
        /// </returns>
        [HttpGet("")]
        public IActionResult GetCars()
        {
            var cars = _carService.GetCars();

            return Ok(cars);
        }

        /// <summary>
        /// GET api/car/{id}
        /// Gets a specific car from the database by it's ID
        /// </summary>
        /// <returns>
        /// An IActionResult.
        /// 200 - OK if the car exists.
        /// 400 - Bad Request if the car does not exist.
        /// </returns>
        [HttpGet("{serialNum:int}")]
        public IActionResult GetCarBySerialNum(int serialNum)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("In Controller");
            Console.WriteLine("==================================================");
            if (serialNum < 1)
            {
                return BadRequest();
            }

            var car = _carService.GetCarBySerialNum(serialNum);

            if (car == null)
            {
                return BadRequest();
            }

            return Ok(car);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromHeader] string sort, string term = null)
        {
            var res = await _esClientProvider.Client.SearchAsync<CarDetailElasticDTO>(x => x
            .Query( q => q.QueryString(qs => qs.Query(term)))
            .Sort(s => s
            .Ascending(sort)

            ));

            if (!res.IsValid)
            {
                throw new InvalidOperationException(res.DebugInformation);
            }

            return Json(res.Documents);
        }
        
        [HttpGet("filters")]
        public IActionResult GetFilters()
        {
            var filters = _carService.GetFilters();

            return Ok(filters);
        }

        [HttpGet("wheel")]
        public IActionResult GetWheels()
        {
            var wheels = _carService.GetWheels();
            return Ok(wheels);
        }

        [HttpGet("fuelType")]
        public IActionResult GetFuelTypes()
        {
            var fuelTypes = _carService.GetFuelTypes();
            return Ok(fuelTypes);     
        }

        [HttpGet("driveSteering")]
        public IActionResult GetDriveSteeringInfos()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("In Controller í DRIVESTEERING");
            Console.WriteLine("==================================================");
            var driveSteering = _carService.GetDriveSteeringInfos();
            return Ok(driveSteering);
            
        }

        
        /*
        * --------------------------------- POST REQUESTS ---------------------------------
        */
        
        // This is temporary functon. Should be a void function inside service
        // that takes all cars with a specific timestamp and puts them into 
        // the elasticsearch database.
        [HttpPost("elasticCreate")]
        public async Task<IActionResult> Create([FromBody]CarDetailElasticDTO car)
        {
            // put our car into the request model
            var carRequestModel = new Nest.IndexRequest<CarDetailElasticDTO>(car);
            
            var res = await _esClientProvider.Client.IndexAsync(carRequestModel);
            if (!res.IsValid)
            {
                throw new InvalidOperationException(res.DebugInformation);
            }

            return Ok();
        }
        
        [HttpPost("")]
        //[AllowAnonymous]
        public IActionResult AddCar([FromBody] CarViewModel newCar)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("In Controller, AddCar");
            Console.WriteLine("==================================================");

            // Check if logged in user is authenticaded as Carsale
            if (HttpContext.Request.Cookies["User"] == CARSALE)
            {
                return AddCarFunction(newCar);
            }

            return Unauthorized();
        }

        public IActionResult AddCarFunction([FromBody] CarViewModel newCar)
        {
            if (newCar == null)
            {
                Console.WriteLine("NEW CAR IS NULL");
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }
            
            // TODO: Check if the car exists, and if it exists call EditCar, not AddCar...
            // as car cannot be twice in the DB

            int registeredCar = _carService.AddCar(newCar);

            return CreatedAtAction("Registered", registeredCar);
        }

        /*
        * --------------------------------- PUT REQUESTS ---------------------------------
        */


        /*
        * --------------------------------- DELETE REQUESTS ---------------------------------
        */

        [HttpDelete("{serialNum:int}")]
        public IActionResult SellCar(int serialNum)
        {
            if (HttpContext.Request.Cookies["User"] == CARSALE)
            {
                return SellCarFunction(serialNum);
            }

            return Unauthorized();
        }
        
        public IActionResult SellCarFunction(int serialNum) 
        {
            if (serialNum > 0)
            {
                var car = _carService.GetCarBySerialNum(serialNum);

                if (car == null)
                {
                    return NotFound();
                }
                else
                {
                    var cars = _carService.SellCar(serialNum);

                    return Ok(cars);
                }
            }
            else
            {
                return StatusCode(412);
            }
        }
    }
}
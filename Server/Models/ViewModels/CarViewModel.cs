using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.ViewModels
{
    public class CarViewModel
    {
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string subType { get; set; }
        public string regNum { get; set; }
        public string year { get; set; }
        public int co2 { get; set; }
        public int weight { get; set; }
        public string color { get; set; }
        public string status { get; set; }
        public string nextCheckUp {get; set; }
        public int carSaleId { get; set; }
        public int price { get; set; }
        public int driven { get; set; }
        public int doors { get; set; }
        public int seating { get; set; }
        public IEnumerable<int> fuelType { get; set; }
        public int drives { get; set; }
        public int cylinders { get; set; }
        public int horsepower { get; set; }
        public bool injection { get; set; }
        public int cc { get; set; }
        public bool onSite { get; set; }
        public IEnumerable<int> wheel { get; set; }
        public int drive { get; set; }
        public int transmission { get; set; }
        public IEnumerable<int> driveSteering { get; set; }
    }
}
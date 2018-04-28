using System;
using System.Collections.Generic;

namespace Bilkaup.Models.DTOModels
{
    public class CarDetailDTO
    {
        public int ID { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string modelType { get; set; }
        public int co2 { get; set; }
        public string color { get; set; }
        public int weight { get; set; }
        public string year { get; set; }
        public int milage { get; set; }
        public string nextCheckup { get; set; }
        public IEnumerable<string> fuelTypes { get; set; }
        public IEnumerable<string> wheels { get; set; }
        public IEnumerable<string> driveSteering { get; set; }
        public int cylinders { get; set; }
        public int cc { get; set; }
        public bool injection { get; set; }
        public int horsepower { get; set; }
        public int price { get; set; }
        public int driven { get; set; }
        public int engineWeight { get; set; }
        public string transmission { get; set; }
        public string drive { get; set; }
        public int seating { get; set; }
        public int doors { get; set; }
        public string moreInfo { get; set; }
        public string status { get; set; }
        public DateTime dateSale { get; set; }
        public DateTime dateUpdate { get; set; }
        public CarSaleBasicDTO carsale { get; set; }
        public int serialNumber { get; set; }
        public IEnumerable<string> extraFeatures { get; set; }

    }
}
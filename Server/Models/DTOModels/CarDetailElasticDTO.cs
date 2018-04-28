using System;
using System.Collections.Generic;

namespace Bilkaup.Models.DTOModels
{
    public class CarDetailElasticDTO
    {
        public int ID { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string modelType { get; set; }
        public int co2 { get; set; }
        public string color { get; set; }
        public int weight { get; set; }
        public int year { get; set; }
        public int milage { get; set; }
        public string nextCheckup { get; set; }
        public IEnumerable<string> fuelTypes { get; set; }
        public int cylinders { get; set; }
        public int cc { get; set; }
        public bool injection { get; set; }
        public int horsepower { get; set; }
        public int price { get; set; }
        public int engineWeight { get; set; }
        // beinskipting/sjálfskipting
        public string transmission { get; set; }
        // gírar
        public int drive { get; set; }
        // framhjóla, afturhjóla, síhjóladrif....
        public IEnumerable<string> driveSteering { get; set; }
        public int seating { get; set; }
        public int doors { get; set; }
        public string moreInfo { get; set; }
        public string status { get; set; }
        public string dateSale { get; set; }
        public string dateUpdate { get; set; }
        public int serialNumber { get; set; }
        public IEnumerable<string> extraFeatures { get; set; }
        public int carsaleId { get; set; }
        public string carsaleName { get; set; }
        public string carsaleEmail { get; set; }
        public string carsalePhoneNum { get; set; }
        public string carsaleAddress { get; set; }
        public string carsaleWebpage { get; set; }
        public bool hybrid { get; set;}

    }
}
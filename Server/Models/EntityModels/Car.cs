using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilkaup.Models.EntityModels
{
    public class Car
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        // Foreign key
        [Required]
        public int ManufacturerID { get; set; }

        // Foreign key
        [Required]
        public int ModelID { get; set; }

        // Foreign key
        public int ModelTypeID { get; set; }

        [Required]
        public string LicenceNumber { get; set; }

        [Required]
        public string Year { get; set; }

        // How many km the car is driven
        public int Milage { get; set; }

        // Registration month and year
        public string NewRegistered { get; set; }

        // When the car is next scheduled for annual check
        public string NextCheckup { get; set; }

        public string Color { get; set; }

        // True if the car has more than one fuel type
        public bool Hybrid { get; set; }

        // Number of cylinders - Icelandic: strokkar
        public int Cylinders { get; set; }

        public int CC { get; set; }

        // Whether the car has injection - Icelandic: innspýting
        public bool Injection { get; set; }

        public int Horsepower { get; set; }

        // CO2 exhaust of the car
        public int CO2 { get; set; }

        // Weight of the engine
        public int EngineWeight { get; set; }

        // Total weigth of the car
        public int Weight { get; set; }

        // Foreign key
        public int TransmissionID { get; set; }

        // Foreign key
        public int DriveID { get; set; }

        // Number of passengers
        public int Seating { get; set; }

        // Number of doors
        public int Doors { get; set; }

        public string MoreInfo { get; set; }

        // From Samgöngustofa, status of the car (whether it's been in use)
        public string Status { get; set; }

        // Inches of the wheels
        public int InchWheels { get; set; }
    }
}
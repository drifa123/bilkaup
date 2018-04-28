using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class DriveSteeringInfoCar
    {
        [Required]
        public int DriveSteeringID { get; set; }
        
        [Required]
        public int CarID { get; set; }
    }
}
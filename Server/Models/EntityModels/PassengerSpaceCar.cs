using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class PassengerSpaceCar
    {
        [Required]
        public int PassengerSpaceID { get; set; }

        [Required]
        public int CarID { get; set; }
    }
}
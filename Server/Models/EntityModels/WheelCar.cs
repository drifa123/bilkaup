using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class WheelCar
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        public int WheelID { get; set; }

        public int Quantity { get; set; }
    }
}
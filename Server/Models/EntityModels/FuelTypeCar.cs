using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class FuelTypeCar
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        public int FuelTypeID { get; set; }
    }
}
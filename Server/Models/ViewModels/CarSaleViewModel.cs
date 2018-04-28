using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.ViewModels
{
    public class CarSaleViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SSN { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNum { get; set; }

        public string Address { get; set; }

        public string Webpage { get; set; }
    }
}
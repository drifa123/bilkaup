using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilkaup.Models.EntityModels
{
    public class CarSale
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string SSN { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNum { get; set; }

        public string Address { get; set; }

        [Required]
        public bool Accepted { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateOfApplication { get; set; }

        public string Webpage { get; set; }

        public string IdentityID { get; set; }

        public ApplicationUser Identity { get; set; }
    }
}
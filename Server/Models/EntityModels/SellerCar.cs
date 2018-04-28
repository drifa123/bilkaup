using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class SellerCar
    {
        [Required]
        public int SellerID { get; set; }
        
        [Required]
        public int CarID { get; set; }

        public string MoreInfo { get; set; }

        public bool SwitchingForExpensive { get; set; }

        public bool SwitchingForCheaper { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class CarSaleOpening
    {
        [Required]
        public int CarSaleID { get; set; }
        
        public string Monday { get; set; }

        public string Tuesday { get; set; }

        public string Wednesday { get; set; }

        public string Thursday { get; set; }

        public string Friday { get; set; }

        public string Saturday { get; set; }

        public string Sunday { get; set; }

        public string OtherInfo { get; set; }
    }
}
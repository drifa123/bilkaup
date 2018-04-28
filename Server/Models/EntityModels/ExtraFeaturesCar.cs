using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class ExtraFeaturesCar
    {
        [Required]
        public int ExtraFeaturesID { get; set; }
        
        [Required]
        public int CarID { get; set; }
    }
}
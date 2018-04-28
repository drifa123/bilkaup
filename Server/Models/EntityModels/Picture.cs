using System.ComponentModel.DataAnnotations;

namespace Bilkaup.Models.EntityModels
{
    public class Picture
    {
        [Required]
        [Key]
        public int CarSerialNum { get; set; }
        
        [Required]
        public string Link { get; set; }

        [Required]
        public bool Primary { get; set; }
    }
}
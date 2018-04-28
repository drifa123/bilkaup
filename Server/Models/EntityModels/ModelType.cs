using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilkaup.Models.EntityModels
{
    public class ModelType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public int ManufID { get; set; }

        [Required]
        public int ModelID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
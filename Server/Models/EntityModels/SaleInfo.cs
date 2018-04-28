using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilkaup.Models.EntityModels
{
    public class SaleInfo
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNum { get; set; }
        
        [Required]
        public int CarSaleID { get; set; }

        [Required]
        public int SellerID { get; set; }

        public int Price { get; set; }
        
        public int OfferPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOnSale { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfUpdate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfSale { get; set; }
        
        public bool OnSite { get; set; }
    }
}
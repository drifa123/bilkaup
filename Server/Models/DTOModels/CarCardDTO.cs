using System;

namespace Bilkaup.Models.DTOModels
{
    public class CarCardDTO
    {
        public int serialNum { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string modelType { get; set; }
        public string imgLink { get; set; }
        public int price { get; set; }
        public int offerPrice { get; set; }
        public int milage {get; set; }
        public string transmission { get; set; }
        public bool onSite { get; set; }
        public string year { get; set; }
        public string regNum { get; set; }
        public DateTime dateOfSale { get; set; }
    }
}
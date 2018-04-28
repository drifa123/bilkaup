using System.Collections.Generic;

namespace Bilkaup.Models.DTOModels
{
    public class CarSaleDetailDTO
    {
        public int ID { get; set; }

        public string name { get; set; }

        public string ssn { get; set; }

        public string email { get; set; }

        public string phoneNum { get; set; }

        public string address { get; set; }

        public string webpage { get; set; }

        public OpeningHoursDTO openingHours { get; set; }

        public IEnumerable<CarCardDTO> cars { get; set; }
    }
}
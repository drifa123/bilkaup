using System;

namespace Bilkaup.Models.DTOModels
{
    public class AdminCarSaleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public bool Accepted { get; set; }
        public bool Active { get; set; }
        public string WebPage { get; set; }
        public DateTime DateOfApplication { get; set; }
    }
}
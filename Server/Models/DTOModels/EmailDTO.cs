using System;

namespace Bilkaup.Models.DTOModels
{
    public class EmailDTO
    {
        public string head { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public string receiverEmail { get; set; }
    }
}
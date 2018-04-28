using System;
using System.Collections.Generic;

namespace Bilkaup.Models.DTOModels
{
    public class ManufacturerFilterDTO
    {
        public string name { get; set; }
        public List<ModelFilterDTO> models { get; set; }
        public bool selected { get; set; }
    }
}
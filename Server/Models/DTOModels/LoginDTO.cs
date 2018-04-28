using System;

namespace Bilkaup.Models.DTOModels
{
    public class LoginDTO
    {
        // ID of the user
        public int ID { get; set; }

        // ID of the user's role; 1 for Admin, 2 for Carsale
        public string role { get; set; }

        // Token of the user
        public string token {get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Bilkaup.Models.AccountViewModels;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Services
{
    public interface IIdentityService 
    {
        /// <summary>
        /// Generates a password that is of the length 10 and has atleast 1 alphanumeric caracter
        /// </summary>
        string GeneratePassword();
    }
}
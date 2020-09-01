using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.Authentication
{
    public class TokenRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

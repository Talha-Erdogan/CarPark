using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.Authentication
{
    public class TokenResponseModel : Data.Entity.Personnel
    {
        public string UserToken { get; set; }

        public int TokenExpirePeriod { get; set; }
    }
}

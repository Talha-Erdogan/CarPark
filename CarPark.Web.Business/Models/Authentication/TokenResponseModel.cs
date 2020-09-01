using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Models.Authentication
{
    public class TokenResponseModel : Business.Models.Personnel.Personnel
    {
        public string UserToken { get; set; }

        public int TokenExpirePeriod { get; set; }
    }
}

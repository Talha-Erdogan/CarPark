using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Common.Model
{
    public class TokenModel
    {
        public long ExpireAsUnixSeconds { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string AuthCodeListAsString { get; set; }
        public string TokenGuid { get; set; }
    }
}

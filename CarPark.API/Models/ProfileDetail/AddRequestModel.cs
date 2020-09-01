using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.ProfileDetail
{
    public class AddRequestModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int AuthId { get; set; }
    }
}

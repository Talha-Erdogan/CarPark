using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.Car
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Plate { get; set; }
    }
}

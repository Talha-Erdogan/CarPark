using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Models.LocationMove
{
    public class LocationWithDetail
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public bool IsEmpty { get; set; }

        //move Id
        public int LocationMove_ID { get; set; }
    }
}

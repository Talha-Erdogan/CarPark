using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Models.LocationMove
{
    public class LocationMoveWithDetail :Data.Entity.LocationMove
    {
        public string Car_Plate { get; set; }
        public string Location_Name { get; set; }

    }
}

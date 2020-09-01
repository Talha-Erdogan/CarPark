using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Models.LocationMove
{
    public class LocationMoveWithDetail :LocationMove
    {
        public string Car_Plate { get; set; }
        public string Location_Name { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Models.LocationMove
{
    public class LocationMoveSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public string Filter_Car_Plate { get; set; }
        public string Filter_Location_Name { get; set; }
    }
}

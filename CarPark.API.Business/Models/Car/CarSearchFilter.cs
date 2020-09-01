using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Models.Car
{
    public class CarSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public string Filter_Plate { get; set; }
    }
}

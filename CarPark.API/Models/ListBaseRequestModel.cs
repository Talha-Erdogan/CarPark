using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models
{
    public class ListBaseRequestModel
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOn { get; set; }
        public string SortDirection { get; set; }
    }
}

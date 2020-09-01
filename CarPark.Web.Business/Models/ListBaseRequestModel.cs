using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Models
{
    public class ListBaseRequestModel
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOn { get; set; }
        public string SortDirection { get; set; }
    }
}

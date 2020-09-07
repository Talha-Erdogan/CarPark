using CarPark.Web.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Models
{
    public class ListBaseViewModel<TData, TFilter>
    {
        public string CurrentLanguageTwoChar { get; set; }

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        public TFilter Filter { get; set; }

        public PaginatedList<TData> DataList { get; set; }

    }
}

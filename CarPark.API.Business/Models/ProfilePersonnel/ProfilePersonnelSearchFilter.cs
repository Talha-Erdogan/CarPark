using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Models.ProfilePersonnel
{
    public class ProfilePersonnelSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters      
        public int Filter_ProfileId { get; set; }
        public string Filter_Personnel_Name { get; set; }
        public string Filter_Personnel_LastName { get; set; }
    }
}

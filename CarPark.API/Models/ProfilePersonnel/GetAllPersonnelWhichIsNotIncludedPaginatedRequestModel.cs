using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.ProfilePersonnel
{
    public class GetAllPersonnelWhichIsNotIncludedPaginatedRequestModel :ListBaseRequestModel
    {
        public int ProfileId { get; set; }
        public string Personnel_Name { get; set; }
        public string Personnel_LastName { get; set; }
    }
}

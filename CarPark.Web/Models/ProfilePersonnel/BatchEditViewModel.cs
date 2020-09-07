using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Models.ProfilePersonnel
{
    public class BatchEditViewModel
    {
        public int? ProfileId { get; set; }
        public List<SelectListItem> ProfileSelectList { get; set; }
        public DefinedPersonnelListViewModel PersonnelList { get; set; }
        public UndefinedPersonnelListViewModel PersonnelWhichIsNotIncludeList { get; set; }

        public string SubmitType { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Models.LocationMove
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        public DateTime? ExitDate { get; set; }

        //other
        public string Location_Name { get; set; }

        //select List
        public List<SelectListItem> CarSelectList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Models.Car
{
    public class AddViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Plate { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Model { get; set; }



        //other
        //public string DisplayLanguage { get; set; }

        //public string SubmitType { get; set; }
    }
}

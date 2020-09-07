using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Web.Models.Auth
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }

        public int? DeletedBy { get; set; }

        //other
        public string DisplayLanguage { get; set; }

        public string SubmitType { get; set; }
    }
}

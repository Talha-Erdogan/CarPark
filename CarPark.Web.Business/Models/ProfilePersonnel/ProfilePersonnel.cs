using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Web.Business.Models.ProfilePersonnel
{
    public class ProfilePersonnel
    {
        public int Id { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int PersonnelId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Web.Business.Models.LocationMove
{
    public class LocationMove
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        public DateTime? ExitDate { get; set; }
    }
}

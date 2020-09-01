using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.LocationMove
{
    public class AddRequestModel
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

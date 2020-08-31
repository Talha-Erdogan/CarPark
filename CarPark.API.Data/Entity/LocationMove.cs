using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPark.API.Data.Entity
{
    [Table("LocationMove")]
    public class LocationMove
    {
        [Key]
        [Column("Id")]
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

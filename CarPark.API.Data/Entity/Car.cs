using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPark.API.Data.Entity
{
    [Table("Car")]
    public class Car
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Plate { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Model { get; set; }
    }
}

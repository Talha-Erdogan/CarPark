using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPark.API.Data.Entity
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string TC { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        [StringLength(200)]
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }
    }
}

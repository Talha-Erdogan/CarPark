using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPark.API.Data.Entity
{
    [Table("ProfileDetail")]
    public class ProfileDetail
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Required]
        public int ProfileId { get; set; }
        [Required]
        public int AuthId { get; set; }
    }
}

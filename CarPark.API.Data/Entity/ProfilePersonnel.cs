using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPark.API.Data.Entity
{
    [Table("ProfilePersonnel")]
   public class ProfilePersonnel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int PersonnelId { get; set; }
    }
}

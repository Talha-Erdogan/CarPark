﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPark.Web.Business.Models.Car
{
    public class AddRequestModel
    {
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

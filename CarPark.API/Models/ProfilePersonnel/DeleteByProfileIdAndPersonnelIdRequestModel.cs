﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.ProfilePersonnel
{
    public class DeleteByProfileIdAndPersonnelIdRequestModel
    {
        public int ProfileId { get; set; }
        public int PersonnelId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models.Personnel
{
    public class GetAllPaginatedRequestModel : ListBaseRequestModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
